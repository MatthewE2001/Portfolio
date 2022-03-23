using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed;
    //public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //i need to move x axis and z axis i assume that is all horizontal?
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 force = new Vector3(moveSpeed, 0.0f, 0.0f);
            gameObject.GetComponent<Rigidbody>().AddForce(force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 force = new Vector3(-moveSpeed, 0.0f, 0.0f);
            gameObject.GetComponent<Rigidbody>().AddForce(force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 force = new Vector3(0.0f, 0.0f, -moveSpeed);
            gameObject.GetComponent<Rigidbody>().AddForce(force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 force = new Vector3(0.0f, 0.0f, moveSpeed);
            gameObject.GetComponent<Rigidbody>().AddForce(force);
        }
    }
}
