using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    Vector3 difference;
    Vector3 offset = Vector3.zero;
    public Transform target; //for read target position
    public float distance = 2f;
    public float height = 1f;
    public float turnSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //difference = transform.position - player.transform.position;
       // offset = new Vector3(target.position.x, (target.position.y + height), (target.position.z - distance));
        //transform.position = offset;
    }
    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void LateUpdate()
    {
        //offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        //transform.position = player.transform.position + offset;
        //transform.LookAt(player.transform.position);
    }

    void MoveCamera()
    {
        //transform.position = player.transform.position + difference;
        //transform.rotation = player.transform.rotation;
    }
}