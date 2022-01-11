using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterScript : MonoBehaviour
{
    public Transform openPosition;
    public Transform closedPosition;

    Vector3 startMarker;
    Vector3 endMarker;

    public float moveSpeed;

    float startTime;
    float journeyLength;

    public GameObject finalMenu;

    bool shouldMove = false;
    bool shouldOpenMenuAfterLerp = false;

    void Update()
    {
        if (shouldMove == true)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;

            float fractionOfJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

            if (Vector3.Distance(transform.position, endMarker) < 0.05f)
            {
                shouldMove = false;

                if (shouldOpenMenuAfterLerp)
                {
                    shouldOpenMenuAfterLerp = false;
                    finalMenu.SetActive(true);
                }
            }
        }
    }

    public void MoveToPosition(bool shouldMoveToOpenPosition, bool shouldOpenMenuWhenDone)
    {
        if (shouldOpenMenuWhenDone)
            shouldOpenMenuAfterLerp = true;
        else
            finalMenu.SetActive(false);

        if(shouldMoveToOpenPosition)
        {
            startMarker = closedPosition.position;
            endMarker = openPosition.position;
        }
        else
        {
            startMarker = openPosition.position;
            endMarker = closedPosition.position;
        }

        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
        shouldMove = true;
    }
}
