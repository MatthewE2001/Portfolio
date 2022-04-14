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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            float tmp = -5.0f;

            //gameObject.transform.rotation.y = gameObject.transform.rotation.y + tmp;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            float tmp = 5.0f;
        }
    }

    void Move()
    {
        //i need to move x axis and z axis i assume that is all horizontal?
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position + movement * Time.deltaTime * moveSpeed);
    }
}
