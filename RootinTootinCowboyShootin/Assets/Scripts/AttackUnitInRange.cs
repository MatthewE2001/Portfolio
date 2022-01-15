using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track of any opposing units in range of itself
//Designed to work when attached to either Player tagged or Enemy tagged game objects
public class AttackUnitInRange : MonoBehaviour
{
    public List<GameObject> targetsInRange;
    public List<GameObject> targetsInLOS;
    public GameObject currentTarget;

    public float range;
    public bool lineOfSightBlocked;

    private IEnumerator listRoutine;
    public float listUpdateIntervalSeconds = 0.5f;

    

    void SetTarget()
    {
        if(targetsInRange.Count != 0)
        {
            currentTarget = targetsInRange[0]; //Targets enemy that has been in range the longest
        }
        else
        {
            currentTarget = null;
        }
    }

    public GameObject GetTarget()
    {
        return currentTarget;
    }

    void CheckLOS()
    {
        for(int i = 0; i < targetsInRange.Count; i++)
        {
            if(targetsInRange[i] == null)
            {
                targetsInRange.RemoveAt(i);
            }
            else
            {
                Ray enemyRay = new Ray(transform.position, targetsInRange[i].transform.position - transform.position);
                Physics.Raycast(enemyRay);

                RaycastHit raycastHit;
                if (Physics.Raycast(enemyRay, out raycastHit, 9) && raycastHit.collider.gameObject.layer == 9)
                {
                    Debug.DrawLine(enemyRay.origin, targetsInRange[i].transform.position, Color.red);

                    lineOfSightBlocked = true;

                    if (targetsInLOS.Contains(targetsInRange[i]))
                    {
                        targetsInLOS.Remove(targetsInRange[i]);
                    }
                }
                else
                {
                    Debug.DrawLine(enemyRay.origin, targetsInRange[i].transform.position, Color.green);
                    if(!targetsInLOS.Contains(targetsInRange[i]))
                    {
                        targetsInLOS.Add(targetsInRange[i]);
                    }
                    lineOfSightBlocked = false;
                    
                }
            }
        }

    }

    void SetTargetToClosest()
    {
        if(targetsInLOS.Count > 0)
        {
            List<float> distances = new List<float>();
            for(int i = 0; i < targetsInLOS.Count; i++)
            {
                if(targetsInLOS[i] == null || !targetsInRange.Contains(targetsInLOS[i]))
                {
                    targetsInLOS.RemoveAt(i);
                }
                else
                {
                    distances.Add(Vector3.Distance(targetsInLOS[i].transform.position, transform.position));
                }
            }
            float min = Mathf.Min(distances.ToArray());
            int indexOfMin = distances.IndexOf(min);

            if(indexOfMin <= targetsInLOS.Count-1 && indexOfMin >= 0) //prevents index out of range
            {
                currentTarget = targetsInLOS[indexOfMin];
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GetComponent<Weapon>().WeaponEventTriggered();
        transform.LookAt(currentTarget.transform);
    }

    public void DeleteEnemy(GameObject enemy)
    {
        int rangeIndex = targetsInRange.IndexOf(enemy);
        int losIndex = targetsInLOS.IndexOf(enemy);
        if(rangeIndex == -1 && losIndex == -1)
        {
            Destroy(enemy);
            return;
        }
        if(rangeIndex != -1)
        {
            targetsInRange.RemoveAt(rangeIndex); 
            Destroy(enemy);
        }
        if (losIndex != -1)
        {
            targetsInLOS.RemoveAt(rangeIndex);
            Destroy(enemy);
        }

    }

    void UpdateList()
    {
        //This is copied almost directly from what Llyn sent me, apart from the contents of the foreach loop -Dan

        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, range);
        targetsInRange.Clear();
        //If collider belongs to an enemy unit it is added to the list of possible targets
        foreach (Collider go in enemyColliders)
        {
            if (go.CompareTag("Enemy") && gameObject.tag == "Player" || (go.CompareTag("Player") && gameObject.tag == "Enemy"))
            {
                targetsInRange.Add(go.gameObject);
            }
        }
    }


    private IEnumerator CheckEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(listUpdateIntervalSeconds);
            //cycles through functions
            UpdateList();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        listRoutine = CheckEnemies();
        StartCoroutine(listRoutine);
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetToClosest();
        CheckLOS();
    }

    private void OnDrawGizmosSelected()
    {
        //if (gameObject.tag == "Player")
        //    Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.15f);
        //else
        //    Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.15f);

        
        //Gizmos.DrawSphere(transform.position, range);

    }

}