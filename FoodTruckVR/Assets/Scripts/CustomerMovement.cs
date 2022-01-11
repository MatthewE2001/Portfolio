using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    Transform spawnPosition;
    Transform foodTruckPosition;
    Transform leavePosition;

    Vector3 startMarker;
    Vector3 endMarker;
    float startTime;
    float journeyLength;

    public float moveSpeed;
    public float delayBeforeLeaving;

    bool shouldMove = false;

    void Update()
    {
        if (shouldMove == true)
        {
            MoveCustomer();
        }
    }

    void MoveCustomer()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;

        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

        if(Vector3.Distance(transform.position, endMarker) < 0.05f)
        {
            shouldMove = false;

            if (endMarker == leavePosition.position)
            {
                GameManager.Instance.SpawnCustomer();
                Destroy(gameObject);
            }
        }
    }

    public void SetPositions(Transform spawn, Transform foodTruck, Transform leave)
    {
        spawnPosition = spawn;
        foodTruckPosition = foodTruck;
        leavePosition = leave;

        startMarker = spawnPosition.position;
        endMarker = foodTruckPosition.position;
    }

    public void StartMovingToFoodTruck()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
        shouldMove = true;
    }

    public void StartLeavingAfterDelay()
    {
        StartCoroutine(LeaveAfterDelay(delayBeforeLeaving));
    }

    IEnumerator LeaveAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        startMarker = foodTruckPosition.position;
        endMarker = leavePosition.position;

        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
        shouldMove = true;
    }
}