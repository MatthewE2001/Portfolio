using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class ButtonFeedback : MonoBehaviour
{
    public Color standardColor;
    public Color highlightedColor;

    public GameObject movingPart;

    public float returnStandardColorDelay;

    public void OnButtonDown()
    {
        ChangeColor(highlightedColor);
        StartCoroutine(GetBackStandardColorInTime(returnStandardColorDelay));
    }

    //public void OnButtonUp()
    //{
    //    GetBackStandardColorInTime(returnStandardColorDelay);
    //}

    private void ChangeColor(Color newColor)
    {
        movingPart.GetComponent<Renderer>().material.color = newColor;
    }

    IEnumerator GetBackStandardColorInTime(float time)
    {
        yield return new WaitForSeconds(time);
        ChangeColor(standardColor);
    }
}
