using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;

    Vector3 moveLocation;
    bool moveActive = false;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartUnitMove();

        if (moving == true)
        {
            Vector3 tmp1 = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y, 0.0f);
            Vector3 tmp2 = new Vector3(moveLocation.x, moveLocation.y, 0.0f);

            gameObject.transform.position = Vector3.Lerp(tmp1, tmp2, speed);
        }

        if (gameObject.transform.position == moveLocation)
        {
            moving = false;
        }
    }

    public void StartUnitMove()
    {
        if (moveActive == true)
        {
            moving = true;

            moveActive = false;
        }
    }

    public void ChangeMoveActive()
    {
        if (moveActive == true)
        {
            moveActive = false;
        }
        else if (moveActive == false)
        {
            moveActive = true;
        }
    }

    public void SetMoveLocation(Vector3 tmpVal)
    {
        moveLocation = tmpVal;
    }
}
