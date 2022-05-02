using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public Vector3 destination;
    public GameObject coverLocation;
    public GameObject fireLocation;
    public float moveSpeed;
    public float moveCooldown;

    float currentMoveCooldown;

    bool moveReady = true;
    bool readyToFire = false; //true?

    // Start is called before the first frame update
    void Start()
    {
        destination = coverLocation.transform.position;
        currentMoveCooldown = moveCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveReady == true)
        {
            ChangeLocation();
            CheckLocation();
        }

        if (moveReady == false)
        {
            currentMoveCooldown -= Time.deltaTime;

            if (currentMoveCooldown <= 0.0f)
            {
                moveReady = true;
                currentMoveCooldown = moveCooldown;
            }
        }

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, moveSpeed);
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
            moveReady = false;
        }

        if (Vector3.Distance(gameObject.transform.position, destination) <= 1.0f && destination == fireLocation.transform.position)
        {
            //fire gun at player
            readyToFire = false;
            moveReady = false;
        }
    }
}