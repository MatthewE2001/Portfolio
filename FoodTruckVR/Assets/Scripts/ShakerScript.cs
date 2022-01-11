using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerScript : GrabbableItemScript
{
    // Start is called before the first frame update
    public Transform shakerTop;
    public GameObject tinyTardigrade;
    public float timeBetweenDispensing;

    [HideInInspector]
    public bool canDispense;
    
    void Awake()
    {
        OnAwake(); // Calling Awake in Base class
        canDispense = true;
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate(); // Calling update in Base class

        //Debug.DrawRay(shakerTop.position, -shakerTop.right);
        //Debug.Log(Vector3.Dot(-shakerTop.right, Vector3.up));

        if (!GetIsOverfried()) // Works only when not overfried
        {
            // "Range of the Dot Product of Two Unit Vectors"
            // https://chortle.ccsu.edu/vectorlessons/vch09/vch09_6.html
            if (Vector3.Dot(-shakerTop.right, Vector3.up) < -0.3f && canDispense)
            {
                StartCoroutine(WaitForTime(timeBetweenDispensing));
                GameObject tardigrade = Instantiate(tinyTardigrade, shakerTop.position, shakerTop.rotation);

                if (GetTimesInEnlarger() == 1)
                {
                    tardigrade.GetComponent<GrabbableItemScript>().Enlarge();
                }
            }
        }
    }

    IEnumerator WaitForTime(float time)
    {
        canDispense = false;
        yield return new WaitForSeconds(time);
        canDispense = true;
    }
}
