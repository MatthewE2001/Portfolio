using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Temporary script meant to be replaced by AI team, currently just picks a random cowboy unit to attack then finds a new cowboy once the first is destroyed
public class EnemyTargetingBeta : MonoBehaviour 
{
    public List<GameObject> cowboys;

    private NavMeshAgent agent;

    public bool pursuingTarget;
    public GameObject currentTarget;

    void SelectTarget()
    {
        int targetIndex = Random.Range(0, cowboys.Count);
        if(cowboys[targetIndex] != null)
        {
            agent.SetDestination(cowboys[targetIndex].transform.position);
        }
        currentTarget = cowboys[targetIndex];
        if(currentTarget == null)
        {
            cowboys.RemoveAt(targetIndex);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cowboys.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        agent = GetComponent<NavMeshAgent>();
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget == null)
        {
            SelectTarget();
        }
        else
        {
            agent.SetDestination(currentTarget.transform.position);
        }

    }
}
