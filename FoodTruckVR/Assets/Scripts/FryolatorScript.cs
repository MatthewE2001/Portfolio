using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryolatorScript : MonoBehaviour
{
    // Frolator orb speed
    public float moveSpeed;
    Vector3 moveDir;

    // Zone where fryolator can move
    GameObject fryolatorZone;

    // Data to make an item inside orb to lerp to orb's center
    float startTime = 0.0f;
    float journeyLength = 0.0f;
    public float cookingFoodMoveSpeed;

    // Item inside orb
    GameObject itemInside;
    GrabbableItemScript itemInsideGrabbableScript;

    // Is currently cooking food inside it
    bool containsItem = false;

    // Bug fix to make sure fryolator never leaves its zone
    float orbLostDelay = 1f;
    float orbLostCountdown = 0f;
    bool leftZone = false;

    // Start is called before the first frame update
    void Start()
    {
        fryolatorZone = transform.parent.gameObject;

        // Randomize move direction
        moveDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        // Moving fryolator
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;

        if(containsItem)
        {
            // Lerp item inside to the center of orb
            journeyLength = Vector3.Distance(itemInside.transform.position, transform.position);
            float distCovered = (Time.time - startTime) * cookingFoodMoveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            itemInside.transform.position = Vector3.Lerp(itemInside.transform.position, transform.position, fractionOfJourney);
        }

        // Making sure fryolator never leaves its zone
        if (leftZone)
        {
            orbLostCountdown += Time.deltaTime;
            if(orbLostCountdown > orbLostDelay)
            {
                moveDir = fryolatorZone.transform.position - transform.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Fryolator got back inside its zone
        if(other.gameObject == fryolatorZone)
        {
            //Debug.Log("Fryolator got back to its zone");

            leftZone = false;
        }

        // When food item gets inside the cooking orb
        if (!containsItem && other.gameObject.GetComponent<GrabbableItemScript>() != null)
        {
            //Debug.Log(other.gameObject.name + " is inside Fryolator");

            if (other.GetComponent<GrabbableItemScript>().isInHand == false)
            {
                containsItem = true;

                itemInside = other.gameObject;
                itemInsideGrabbableScript = other.GetComponent<GrabbableItemScript>();
                itemInsideGrabbableScript.StartFrying(this.gameObject);

                startTime = Time.time;
                journeyLength = Vector3.Distance(itemInside.transform.position, transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When leaves its zone
        if (other.gameObject == fryolatorZone)
        {
            //Debug.Log("Fryolator left its zone");

            leftZone = true;
            moveDir = Quaternion.Euler(Random.Range(-60f, 60f), Random.Range(-60f, 60f), Random.Range(-60f, 60f)) *
                (fryolatorZone.transform.position - transform.position); // Randomize movement direction
        }

        // When food item is taken away from fryolator
        if (containsItem && other.gameObject == itemInside)
        {
            //Debug.Log(other.gameObject.name + " left Fryolator");
            containsItem = false;

            itemInsideGrabbableScript.StopFrying();
        }

    }
}