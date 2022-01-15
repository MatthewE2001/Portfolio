using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlacedAbility : MonoBehaviour
{
    public float range;
    public GameObject abilityPrefab;
    public bool usingAbility;
    bool abilityUsed;
    bool placeChosen;
    Vector3 abilityPlacementLocations;

    public void UseAbility()
    {
        usingAbility = true;
        //base.UseAbility();
        Debug.Log("Dynamite use");
    }

    // Start is called before the first frame update
    void Start()
    {
        //canUse = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (usingAbility)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    abilityPlacementLocations = hit.point;
                    Debug.Log("Ability spot chosen");
                    abilityUsed = true;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                usingAbility = false;
            }
            if(abilityUsed)
            {
                MoveTowardUseSpot();
                AttemptPlaceItem();
            }
        }
    }

    void MoveTowardUseSpot()
    {
        Debug.Log("Move toward set");
        this.GetComponent<NavMeshAgent>().SetDestination(abilityPlacementLocations);
    }

    void AttemptPlaceItem()
    {
        float dist = Vector3.Distance(abilityPlacementLocations, transform.position);
        if (dist < range)
        {
            abilityUsed = false;
            Debug.Log("Placed Ability");
            Instantiate(abilityPrefab, abilityPlacementLocations, Quaternion.identity);
            usingAbility = false;
            this.GetComponent<NavMeshAgent>().SetDestination(this.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
