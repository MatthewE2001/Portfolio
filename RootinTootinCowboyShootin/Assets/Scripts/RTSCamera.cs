using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    public GameObject followObject;
    public float xMove;
    public float zMove;
    public float cameraSpeed;
    public bool rotateLeft, rotateRight;
    //public bool zoomIn, zoomOut;
    public float rotSpeed;
    public float zoomSpeed;
    public float scrollAxis;

    public float maxCameraScale;
    public float minCameraScale;

    public float northBoundary;
    public float eastBoundary;
    public float southBoundary;
    public float westBoundary;
    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");
        rotateLeft = Input.GetKey(KeyCode.Q);
        rotateRight = Input.GetKey(KeyCode.E);
        //zoomIn = Input.GetKey(KeyCode.Equals);
        scrollAxis = Input.GetAxis("Mouse ScrollWheel");
        //zoomOut = Input.GetKey(KeyCode.Minus
    }

    private void RTSCameraMovement() //boundary collision is not the smoothest but should work fine
    {
        Vector3 xCameraMovement = Camera.main.transform.right * xMove * cameraSpeed;
        Vector3 zCameraMovement = Camera.main.transform.forward;
        zCameraMovement = new Vector3(zCameraMovement.x, 0f, zCameraMovement.z) * zMove * cameraSpeed; //Sets Y value to 0 so the camera pans instead of zooms
        Vector3 newPosition = transform.position + (xCameraMovement + zCameraMovement);

        if(!(newPosition.x > eastBoundary && newPosition.x < westBoundary)) //if the boundaries are being broken with the new x value
        {
            newPosition.x = transform.position.x; 
            //denies the new x coordinate, but allows the new z coordinate to still be 
            //changed, this should help the camera feel better to move around the 
            //borders of the level
        }
        if(!(newPosition.z < southBoundary && newPosition.z > northBoundary)) //same as above but for z value
        {
            newPosition.z = transform.position.z;
        }
        //no need for a y value statement since y does not change in this function

        transform.position = newPosition;
        
    }   

    private void CameraRotation()
    {
        if(rotateLeft && rotateRight)
        {
            //does nothing
        }
        else if(rotateLeft)
        {
            transform.Rotate(0f, rotSpeed, 0f);
        }
        else if(rotateRight)
        {
            transform.Rotate(0f, rotSpeed * -1f, 0f);
        }
    }

    private void CameraScale()
    {
        transform.localScale -= new Vector3(scrollAxis, scrollAxis, scrollAxis) * zoomSpeed;
        if(transform.localScale.x < minCameraScale)
        {
            transform.localScale = new Vector3(minCameraScale, minCameraScale - 0.5f, minCameraScale);

        }
        if (transform.localScale.x > 2f)
        {
            transform.localScale = new Vector3(maxCameraScale, maxCameraScale - 0.5f, maxCameraScale);

        }
        //if (zoomIn && zoomOut)
        //{
        //    //does nothing
        //}
        //else if (zoomIn)
        //{
        //    transform.localScale -= new Vector3(zoomSpeed, zoomSpeed, zoomSpeed);
        //}
        //else if (zoomOut)
        //{
        //    transform.localScale += new Vector3(zoomSpeed, zoomSpeed, zoomSpeed);
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 xCameraMovement = Camera.main.transform.right;
        //Vector3 zCameraMovement = Camera.main.transform.forward;
        //zCameraMovement = new Vector3(zCameraMovement.x, 0f, zCameraMovement.z) * zMove * cameraSpeed;
        //GameObject forward = new GameObject();
        //GameObject right = new GameObject();
        //forward.transform.position = zCameraMovement;
        //right.transform.position = xCameraMovement;
        //Instantiate(forward, Camera.main.transform);
        //Instantiate(right, Camera.main.transform);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        RTSCameraMovement();
        CameraRotation();
        CameraScale();
    }
}
