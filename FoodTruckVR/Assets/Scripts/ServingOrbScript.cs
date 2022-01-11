using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingOrbScript : MonoBehaviour
{
    // Serving orb speed
    public float orbMoveSpeed;

    // Has an item inside it
    bool containsItem = false;

    // Final destination of orb when it has correct item in it
    public Transform finalDestination;

    // Item inside the orb
    GameObject itemInside;
    GrabbableItemScript itemInsideGrabbableScript;

    // Needed food's name
    public IngredientsManager.GrabbableItemName requiredItemName;

    // Data to make an item inside orb to lerp to orb's center
    float startTime = 0.0f;
    float journeyLength = 0.0f;
    public float servedFoodMoveSpeed;

    // Colors of orb depending on the item inside it
    public Color wrongItemColor;
    public Color correctItemColor;
    public Color noItemColor;

    // Start is called before the first frame update
    void Start()
    {
        //finalDestination = GameObject.Find("ServingOrbDestination").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (containsItem)
        {
            gameObject.GetComponent<Renderer>().material.color = correctItemColor;

            //moving sphere if an item is put inside
            float step = orbMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, finalDestination.position, step);

            // Lerp item inside to the center of orb
            journeyLength = Vector3.Distance(itemInside.transform.position, transform.position);
            float distCovered = (Time.time - startTime) * servedFoodMoveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            itemInside.transform.position = Vector3.Lerp(itemInside.transform.position, transform.position, fractionOfJourney);
        }

        // Change color to standard one if no item inside
        if(!containsItem)
            gameObject.GetComponent<Renderer>().material.color = noItemColor;
    }

    private void OnTriggerStay(Collider other)
    {
        // When food item gets inside the serving orb
        if (!containsItem && other.gameObject.GetComponent<GrabbableItemScript>() != null)
        {
            //Debug.Log(other.gameObject.name + " is inside Serving Orb");

            // Checking if player is not holding the item
            if (other.GetComponent<GrabbableItemScript>().isInHand == false)
            {
                containsItem = true;

                itemInside = other.gameObject;
                itemInsideGrabbableScript = other.GetComponent<GrabbableItemScript>();
                itemInsideGrabbableScript.OnEnterOrb(this.gameObject);

                startTime = Time.time;
                journeyLength = Vector3.Distance(itemInside.transform.position, transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When food item is taken away from the orb
        if (containsItem && other.gameObject == itemInside)
        {
            //Debug.Log(other.gameObject.name + " left Serving Orb");
            containsItem = false;

            itemInsideGrabbableScript.OnExitOrb();
        }
    }
}

//storing this code at the bottom
//if (itemInsideGrabbableScript.itemName == requiredItemName)
//{
//    gameObject.GetComponent<Renderer>().material.color = correctItemColor;
//}
//else // if food item inside is wrong
//{
//    gameObject.GetComponent<Renderer>().material.color = wrongItemColor;
//}