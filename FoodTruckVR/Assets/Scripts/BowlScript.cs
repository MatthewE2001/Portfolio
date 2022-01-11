using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class BowlScript : GrabbableItemScript
{
    public Transform[] spots;

    bool isFullOfTardigrades = false;
    int numOfTardigrades = 0;

    void Awake()
    {
        OnAwake(); // Calling Awake in Base class
    }

    void Update()
    {
        OnUpdate(); // Calling update in Base class
        BowlFrying();
    }

    void BowlFrying()
    {
        if (isFrying && isFullOfTardigrades && timeSpentInFryolator > timeToCookInFryolator)
        {
            ChangeColorOfTardigrades(friedColor);
            itemName = IngredientsManager.GrabbableItemName.FriedBowlOfTardigrades;
        }
        if (isFullOfTardigrades && timeSpentInFryolator > timeToOvercookInFryolator)
        {
            ChangeColorOfTardigrades(Color.black);
            itemName = IngredientsManager.GrabbableItemName.Charcoal;
        }
    }

    void ChangeColorOfTardigrades(Color color)
    {
        if(isFullOfTardigrades)
        {
            for (int i = 0; i < spots.Length; i++)
            {
                spots[i].GetChild(0).GetComponent<Renderer>().material.color = color;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //?make the tardigrades move to the position?
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);

        //Debug.Log("Step 0");
        if (!GetIsOverfried()) // Bowl works only when not overfried
        {
            //Debug.Log("Step 1: " + collision.gameObject);
            if (collision.gameObject.GetComponent<GrabbableItemScript>() != null) // if the thing this bowl collides with is a grabbable item
            {
                GrabbableItemScript collisionGrabScript = collision.gameObject.GetComponent<GrabbableItemScript>();

                //Debug.Log("Step 2");
                if (collisionGrabScript.itemName == IngredientsManager.GrabbableItemName.Tardigrade
                    || collisionGrabScript.itemName == IngredientsManager.GrabbableItemName.FriedTardigrade)
                {
                    //Debug.Log("Step 3");
                    if ((GetTimesInEnlarger() == 0 && collisionGrabScript.GetTimesInEnlarger() == 1) ||
                        (GetTimesInEnlarger() == 1 && collisionGrabScript.GetTimesInEnlarger() == 2))
                    {
                        //Debug.Log("Step 4");
                        for (int i = 0; i < spots.Length; i++)
                        {
                            //Vector3 originalScale = collision.transform.root.localScale;
                            if (spots[i].childCount == 0)
                            {
                                numOfTardigrades++;
                                if (numOfTardigrades == spots.Length)
                                {
                                    timeSpentInFryolator = 0f;
                                    isFullOfTardigrades = true;
                                    canHaveFlavor = true;
                                    AddFlavor(IngredientsManager.FlavorType.Theta, 1);
                                }

                                //Debug.Log("picked up");
                                collision.gameObject.transform.position = spots[i].position;
                                collision.gameObject.transform.parent = spots[i];
                                collision.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true; // Change later

                                /*float newYScale = collision.transform.localScale.y / transform.localScale.y;
                                collisionParent.localScale = new Vector3(collision.transform.localScale.x,
                                                                             newYScale,
                                                                             collision.transform.localScale.z);*/
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}