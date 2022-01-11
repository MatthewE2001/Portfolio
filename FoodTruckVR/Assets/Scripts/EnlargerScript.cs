using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform spawnerLoc;
    //RenderBuffer tempBody;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GrabbableItemScript>() != null)
        {
            GrabbableItemScript grabScript = other.gameObject.GetComponent<GrabbableItemScript>();
            if (!grabScript.isInHand)
            {
                if (grabScript.CanBeEnlarged())
                {
                    grabScript.Enlarge();

                    if (grabScript.canHaveFlavor && !grabScript.GetIsOverfried())
                    {
                        grabScript.AddFlavor(IngredientsManager.FlavorType.Theta, 1);
                        grabScript.RemoveFlavor(IngredientsManager.FlavorType.Omega, 1);
                    }
                }

                other.transform.position = spawnerLoc.position;
                if (other.GetComponent<Rigidbody>() != null)
                    other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}