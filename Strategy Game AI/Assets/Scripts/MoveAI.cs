using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAI : MonoBehaviour
{
    public Vector3 movePosition;
    public float moveSpeed;

    public GameObject teamManager;

    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position == movePosition)
        {
            ChangeIsMoving();
            teamManager.GetComponent<TeamManager>().SetLocationReached();
        }

        if (isMoving == true)
        {
            MoveTowardsLocation();
        }
    }

    public void MoveTowardsLocation()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, movePosition, moveSpeed);
    }

    public void ChangeIsMoving()
    {
        if (isMoving == true)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    public void SetLocation(Vector3 location)
    {
        movePosition = location;
    }
}