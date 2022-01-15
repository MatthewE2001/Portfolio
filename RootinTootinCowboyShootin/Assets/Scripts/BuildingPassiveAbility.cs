using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingPassiveAbility : MonoBehaviour
{
    public float maxBenefitRange = 20;
    public float baseSpeed;
    public float baseAcceleration;
    public float maxMultiplier = 5;
    public float minDistance;
    public float speed;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        baseSpeed = agent.speed;
        baseAcceleration = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxBenefitRange);
        minDistance = maxBenefitRange;
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Building")
            {
                float newDist = Vector3.Distance(collider.transform.position, transform.position);
                if (newDist < minDistance)
                {
                    minDistance = newDist;
                }
            }
        }
        float distanceRatio = minDistance / maxBenefitRange;
        speed = distanceRatio * maxMultiplier;
        agent.speed = baseSpeed * speed;
        agent.acceleration = agent.speed * 2;
        agent.angularSpeed = agent.speed * 5.2f;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxBenefitRange);
    }
}
