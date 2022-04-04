using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject camera; //Camera camera?
    public Transform thirdPersonPosition;
    public Transform firstPersonPosition;

    Vector3 distance;
    bool isThirdPerson = true;

    // Start is called before the first frame update
    void Start()
    {
        distance = player.transform.position - camera.transform.position;
        //camera.transform.position = 
    }

    // Update is called once per frame
    void Update()
    {
        distance = distance - camera.transform.position;

        if (Input.GetKey(KeyCode.C))
        {
            ChangeThirdPersonStatus();
            AdjustFirstThirdPerson();
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        //camera.gameObject.position = distance;
    }

    void ChangeThirdPersonStatus()
    {
        if (isThirdPerson == true)
        {
            isThirdPerson = false;
        }
        else
        {
            isThirdPerson = true;
        }
    }

    void AdjustFirstThirdPerson()
    {
        if (isThirdPerson == true)
        {
            //want it to lerp into a first person view basically
            camera.transform.position = thirdPersonPosition.position;
        }
        else if (isThirdPerson == false)
        {
            //lerp back to a third person camera
            camera.transform.position = firstPersonPosition.position;
        }
    }
}
