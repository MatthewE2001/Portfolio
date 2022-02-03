using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Camera mainCamera;
    public float xMove;
    public float yMove;

    Vector3 tmpPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        if (Input.GetKey(KeyCode.W))
        {
            tmpPosition = new Vector3(0.0f, yMove, 0.0f);
            mainCamera.transform.position = mainCamera.transform.position + tmpPosition;
        }

        if (Input.GetKey(KeyCode.S))
        {
            tmpPosition = new Vector3(0.0f, -yMove, 0.0f);
            mainCamera.transform.position = mainCamera.transform.position + tmpPosition;
        }

        if (Input.GetKey(KeyCode.D))
        {
            tmpPosition = new Vector3(xMove, 0.0f, 0.0f);
            mainCamera.transform.position = mainCamera.transform.position + tmpPosition;
        }

        if (Input.GetKey(KeyCode.A))
        {
            tmpPosition = new Vector3(-xMove, 0.0f, 0.0f);
            mainCamera.transform.position = mainCamera.transform.position + tmpPosition;
        }
    }
}
