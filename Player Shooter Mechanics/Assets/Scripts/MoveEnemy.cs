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
    }

    void ChangeLocation()
    {
        if (readyToFire == false)
        {

        }
    }
}
