using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Valve.VR.InteractionSystem;


public class MixingMachineScript : MonoBehaviour
{
    public HoverButton hoverButton;

    GameObject objectOne = null;
    GameObject objectTwo = null;

    public Transform spawnLoc;
    
    int numOfObjectsInside = 0;
    bool isCorrectNumOfItemsInside = false;

    GameObject grabbableItemsParent;        // Empty GameObject on the scene to use as a parent

    [Header("Light Indicator related")]
    public GameObject lightIndicator;
    public Color readyToMixLightColor;
    public Color notReadyToMixLightColor;

    void Start()
    {
        grabbableItemsParent = GameObject.Find("GrabbableItemsParent");

        hoverButton.onButtonDown.AddListener(OnButtonDown);
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfObjectsInside == 2)
        {
            isCorrectNumOfItemsInside = true;
        }
        else
        {
            isCorrectNumOfItemsInside = false;
        }

        ChangeLightIndicatorColor(isCorrectNumOfItemsInside);

        //Debug.Log(numOfObjectsInside);
    }

    void OnButtonDown(Hand hand)
    {
        if (isCorrectNumOfItemsInside)
            TryToMix();
    }

    void TryToMix()
    {
        if (objectOne != null && objectTwo != null)
        {
            IngredientsManager.GrabbableItemName resultItemName = IngredientsManager.Instance.GetResultOfMixing(
                    objectOne.GetComponent<GrabbableItemScript>().itemName,
                    objectTwo.GetComponent<GrabbableItemScript>().itemName);

            GameObject finalResult;

            finalResult = IngredientsManager.Instance.SpawnItem(resultItemName);
            finalResult.transform.position = spawnLoc.position;
            finalResult.transform.SetParent(grabbableItemsParent.transform);

            finalResult.GetComponent<GrabbableItemScript>().canHaveFlavor = true;
            finalResult.GetComponent<GrabbableItemScript>().ZeroOutFlavors();
            if (resultItemName != IngredientsManager.GrabbableItemName.SpaceGarbage)
            {
                finalResult.GetComponent<GrabbableItemScript>().AddFlavor(IngredientsManager.FlavorType.Alpha,
                    objectOne.GetComponent<GrabbableItemScript>().flavorAlpha);
                finalResult.GetComponent<GrabbableItemScript>().AddFlavor(IngredientsManager.FlavorType.Alpha,
                    objectTwo.GetComponent<GrabbableItemScript>().flavorAlpha);

                finalResult.GetComponent<GrabbableItemScript>().AddFlavor(IngredientsManager.FlavorType.Omega,
                    objectOne.GetComponent<GrabbableItemScript>().flavorOmega);
                finalResult.GetComponent<GrabbableItemScript>().AddFlavor(IngredientsManager.FlavorType.Omega,
                    objectTwo.GetComponent<GrabbableItemScript>().flavorOmega);

                finalResult.GetComponent<GrabbableItemScript>().AddFlavor(IngredientsManager.FlavorType.Sigma,
                    objectOne.GetComponent<GrabbableItemScript>().flavorSigma);
                finalResult.GetComponent<GrabbableItemScript>().AddFlavor(IngredientsManager.FlavorType.Sigma,
                    objectTwo.GetComponent<GrabbableItemScript>().flavorSigma);

                finalResult.GetComponent<GrabbableItemScript>().AddFlavor(IngredientsManager.FlavorType.Theta,
                    objectOne.GetComponent<GrabbableItemScript>().flavorTheta);
                finalResult.GetComponent<GrabbableItemScript>().AddFlavor(IngredientsManager.FlavorType.Theta,
                    objectTwo.GetComponent<GrabbableItemScript>().flavorTheta);
            }

            Destroy(objectOne);
            Destroy(objectTwo);
            objectOne = null;
            objectTwo = null;
            numOfObjectsInside = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GrabbableItemScript>() != null)
        {
            numOfObjectsInside++;
            //Debug.Log(other.gameObject.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<GrabbableItemScript>() != null)
        {
            numOfObjectsInside--;

            if (other.gameObject == objectOne)
                objectOne = null;
            if (other.gameObject == objectTwo)
                objectTwo = null;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<GrabbableItemScript>() != null)
        {
            if (isCorrectNumOfItemsInside)
            {
                if (objectOne == null && objectTwo != other.gameObject)
                {
                    objectOne = other.gameObject;
                }
                else if (objectTwo == null && objectOne != other.gameObject)
                {
                    objectTwo = other.gameObject;
                }
            }
        }
    }

    void ChangeLightIndicatorColor(bool isReadyToMix)
    {
        if (isReadyToMix)
        {
            lightIndicator.GetComponent<Renderer>().material.color = readyToMixLightColor;
            lightIndicator.GetComponentInChildren<Light>().color = readyToMixLightColor;
        }
        else
        {
            lightIndicator.GetComponent<Renderer>().material.color = notReadyToMixLightColor;
            lightIndicator.GetComponentInChildren<Light>().color = notReadyToMixLightColor;
        }
    }
}
