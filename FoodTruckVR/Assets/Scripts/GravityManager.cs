using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class GravityManager : MonoBehaviour
{

    public Transform knob;
    //public Interactable knobInteracable;
    //public CircularDrive circDrive;
    //bool grabbed;

    public float PlanetaryGravity = -9.8f;
    float dialPos;
    float lastDialPos;
    // Start is called before the first frame update
    void Start()
    {
        //knobInteracable.onAttachedToHand += KnobInteracable_onAttachedToHand;
        //knobInteracable.onDetachedFromHand += KnobInteracable_onDetachedFromHand;

        UpdateGravity(new Vector3(0.0f, PlanetaryGravity, 0.0f)); // set starting gravity
        dialPos = knob.rotation.eulerAngles.y;
    }
 
    // Update is called once per frame
    void Update()
    {
        dialPos = knob.rotation.eulerAngles.y; // sample dial rotation
        if (dialPos != lastDialPos) // if the dial has turned
        {
            //Debug.Log(dialPos);
            UpdateGravity(new Vector3(0.0f, (1-(dialPos/180))*PlanetaryGravity, 0.0f)); // update gravity according to dial
        }
        lastDialPos = dialPos; // record current rotation for next run
    }

    private void UpdateGravity(Vector3 newGrav)
    {
        Physics.gravity = newGrav;
    }


    /*
    private void KnobInteracable_onDetachedFromHand(Hand hand)
    {
        grabbed = false;
        Debug.Log(grabbed);

    }

    private void KnobInteracable_onAttachedToHand(Hand hand)
    {
        grabbed = true;
        Debug.Log(grabbed);

    }
    */
}
