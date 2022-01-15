using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnitMovement : MonoBehaviour //
{

    public Camera cam;

    public NavMeshAgent agent;

    //public bool selected = false;

    public bool pursuingTarget;
    public bool targetSet;

    public GameObject currentTarget;
    public GameObject movementReticlePrefab;
    private GameObject movementReticleObject;
    private CharacterHealthScript unitStats;

    public GameObject circleSel;
    MeshRenderer cRender;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cRender = circleSel.GetComponent<MeshRenderer>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pursuingTarget && currentTarget != null && targetSet == false)
        {
            agent.SetDestination(currentTarget.transform.position);
            Debug.Log("Pursuing " + currentTarget.name);
            targetSet = true;
        }
        else if (GameObject.FindObjectOfType<UnitSelector>().selectedUnit == gameObject)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.tag == "Enemy")
                    {
                        pursuingTarget = true;
                        currentTarget = hit.collider.gameObject;
                    }
                    else
                    {
                        agent.SetDestination(hit.point);
                        if(movementReticleObject == null)
                        {
                            movementReticleObject = Instantiate(movementReticlePrefab, null);
                        }
                        movementReticleObject.SetActive(true);
                        movementReticleObject.transform.position = agent.destination;

                        pursuingTarget = false;
                    }
                }
            }
        }
        else
        {
            cRender.enabled = false;
        }
        if (Vector3.Distance(transform.position, agent.destination) <= 1f)
        {
            if(movementReticleObject != null)
            {
                movementReticleObject.SetActive(false);
            }
        }
    }

    private void OnMouseDown()
    {
        GameObject.FindObjectOfType<UnitSelector>().SelectUnit(gameObject);
        cRender.enabled = true;
    }

    public void UpdateCam()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void SelectThisUnit()
    {

    }
}
