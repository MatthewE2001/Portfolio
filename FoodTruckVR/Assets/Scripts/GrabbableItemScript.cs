using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GrabbableItemScript : MonoBehaviour
{
    [Header("General")]
    public IngredientsManager.GrabbableItemName itemName;               // Item's name
    public IngredientsManager.GrabbableItemType itemType;               // Item's type
    [HideInInspector] public bool isInHand;                             // Is attached to hand?
    GameObject grabbableItemsParent;                                    // Empty GameObject on the scene to use as a parent
    bool shouldUnparent = false;                                        // If should unparent from everything when not attached to hand

    [Header("Flavor related")]
    public bool canHaveFlavor;                                          //
    public int flavorOmega;                                             //
    public int flavorAlpha;                                             //
    public int flavorSigma;                                             //
    public int flavorTheta;                                             //

    [Header("Fryolator related")]
    public bool canBeFried;                                             // Can this item be fried?
    public IngredientsManager.GrabbableItemName itemNameAfterFryolator; // Item's name when fried
    public float timeToCookInFryolator;                                 // Time it takes an item to fry
    public float timeToOvercookInFryolator;                             // Time it takes an item to overfry
    protected float timeSpentInFryolator = 0f;                                    // Time spent in fryolator
    public Color friedColor;                                            // Color of item when fried
    bool isFried = false;                                               // Is it fryed?
    [HideInInspector] public bool isFrying;                             // Is it currently frying?
    bool isOverfried = false;                                           // Is it overfried?

    [Header("Enlarger related")]
    public bool canBeEnlarged;                                          // Can this item be enlarged?
    public float enlargeTimesXPerUse;                                   // How much should it be enlarged every use?
    public int maxTimesInEnlarger;                                      // How many times can this item be enlarged?
    int timesInEnlarger = 0;                                            // Number of times item was Enlarged

    GameObject wristHUD;

    // Called only if there are no derived class
    private void Awake()
    {
        OnAwake();
    }

    // Called only if there are no derived class
    private void Update()
    {
        OnUpdate();
    }

    // OnUpdate is called in Derived Class
    public void OnAwake()
    {
        isInHand = false;
        isFrying = false;
        grabbableItemsParent = GameObject.Find("GrabbableItemsParent");

        wristHUD = GameManager.Instance.GetWristHUDGameObject();
    }

    // OnUpdate is called once per frame in Derived Class
    public void OnUpdate()
    {
        if (isFrying)
        {
            timeSpentInFryolator += Time.deltaTime;

            if (!isFried && timeSpentInFryolator > timeToCookInFryolator)
            {
                isFried = true;
                itemName = itemNameAfterFryolator;
                gameObject.GetComponent<Renderer>().material.color = friedColor;

                if (canHaveFlavor)
                {
                    AddFlavor(IngredientsManager.FlavorType.Omega, 1);
                    RemoveFlavor(IngredientsManager.FlavorType.Sigma, 1);
                }
            }

            if (timeSpentInFryolator > timeToOvercookInFryolator)
            {
                BecomeOverfried();
            }
        }

        if (shouldUnparent)
        {
            if (!isInHand)
            {
                UnparentFromEverything();
                shouldUnparent = false;
            }
        }
    }

    // HUD Function
    public void UpdateHud()
    {
        wristHUD.SetActive(true);
        wristHUD.GetComponent<WristHUD>().UpdateHUD(itemName.ToString(), isFried,
            timesInEnlarger, canHaveFlavor, flavorAlpha, flavorOmega,
            flavorSigma, flavorTheta);
    }
     public void ResetHUD()
    {
        wristHUD.SetActive(false);
    }

    public void StartFrying(GameObject fryolator)
    {
        if (canBeFried)
        {
            isFrying = true;
        }
        else
        {
            BecomeOverfried();
        }

        ParentToTransform(fryolator.transform);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void StopFrying()
    {
        isFrying = false;
        shouldUnparent = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void BecomeOverfried()
    {
        isOverfried = true;
        itemName = IngredientsManager.GrabbableItemName.Charcoal;
        gameObject.GetComponent<Renderer>().material.color = Color.black;
        ZeroOutFlavors();
    }

    public bool GetIsOverfried()
    {
        return isOverfried;
    }

    public void OnEnterOrb(GameObject servingOrb)
    {
        ParentToTransform(servingOrb.transform);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void OnExitOrb()
    {
        shouldUnparent = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void OnEnterOrbWithoutParenting()
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("Something went wrong trying to access the object rigidbody");
        }
    }

    public void OnExitOrbWithoutParenting()
    {
        if(gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("Something went wrong trying to access the object rigidbody");
        }
    }

    public bool CanBeEnlarged()
    {
        if (maxTimesInEnlarger > timesInEnlarger && canBeEnlarged)
            return true;

        return false;
    }

    public void Enlarge()
    {
        transform.localScale = transform.localScale * enlargeTimesXPerUse;
        timesInEnlarger++;
    }

    public int GetTimesInEnlarger()
    {
        return timesInEnlarger;
    }

    public void ParentToTransform(Transform newParent)
    {
        transform.SetParent(newParent);
    }

    public void UnparentFromEverything()
    {
        transform.SetParent(grabbableItemsParent.transform);
    }

    public void AttachToHand()
    {
        isInHand = true;
    }

    public void DetachFromHand()
    {
        isInHand = false;
    }

    public void AddFlavor(IngredientsManager.FlavorType flavorType, int amount)
    {
        switch(flavorType)
        {
            case IngredientsManager.FlavorType.Alpha:
                {
                    flavorAlpha += amount;
                    break;
                }
            case IngredientsManager.FlavorType.Omega:
                {
                    flavorOmega += amount;
                    break;
                }
            case IngredientsManager.FlavorType.Sigma:
                {
                    flavorSigma += amount;
                    break;
                }
            case IngredientsManager.FlavorType.Theta:
                {
                    flavorTheta += amount;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void RemoveFlavor(IngredientsManager.FlavorType flavorType, int amount)
    {
        switch (flavorType)
        {
            case IngredientsManager.FlavorType.Alpha:
                {
                    flavorAlpha -= amount;

                    if (flavorAlpha < 0)
                        flavorAlpha = 0;

                    break;
                }
            case IngredientsManager.FlavorType.Omega:
                {
                    flavorOmega -= amount;

                    if (flavorOmega < 0)
                        flavorOmega = 0;

                    break;
                }
            case IngredientsManager.FlavorType.Sigma:
                {
                    flavorSigma -= amount;

                    if (flavorSigma < 0)
                        flavorSigma = 0;

                    break;
                }
            case IngredientsManager.FlavorType.Theta:
                {
                    flavorTheta -= amount;

                    if (flavorTheta < 0)
                        flavorTheta = 0;

                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void ZeroOutFlavors()
    {
        flavorAlpha = 0;
        flavorOmega = 0;
        flavorSigma = 0;
        flavorTheta = 0;
    }
}
