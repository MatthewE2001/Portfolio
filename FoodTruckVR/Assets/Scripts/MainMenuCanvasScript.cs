using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasScript : MonoBehaviour
{
    Vector3 startMarker;
    Vector3 endMarker;

    float lerpSpeed;

    private float startTime;
    private float journeyLength;

    bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * lerpSpeed;

            float fractionOfJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

            if (Vector3.Distance(transform.position, endMarker) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    public void LerpToPosition(Transform finalPosition, float speed)
    {
        startTime = Time.time;
        startMarker = transform.position;
        endMarker = finalPosition.position;
        lerpSpeed = speed;
        journeyLength = Vector3.Distance(transform.position, endMarker);

        isMoving = true;
    }
}
