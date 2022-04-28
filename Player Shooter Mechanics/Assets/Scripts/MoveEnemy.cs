using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public Vector3 destination;
    public Vector3 fireDestination;
    public GameObject coverLocation;
    public GameObject fireLocation;

    bool readyToFire = false; //true?

    // Start is called before the first frame update
    void Start()
    {
        destination = coverLocation.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLocation();
        CheckLocation();
    }

    void ChangeLocation()
    {
        if (readyToFire == false)
        {
            destination = coverLocation.transform.position;
        }

        if (readyToFire == true)
        {
            destination = fireLocation.transform.position;
        }
    }

    void CheckLocation()
    {
        if (Vector3.Distance(gameObject.transform.position, destination) <= 1.0f && destination == coverLocation.transform.position)
        {
            readyToFire = true;
        }

        if (Vector3.Distance(gameObject.transform.position, destination) <= 1.0f && destination == fireLocation.transform.position)
        {
            //fire gun at player
            readyToFire = false;
        }
    }
}