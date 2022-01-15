using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    public List<GameObject> targetsInRange;
    public List<GameObject> targetsInLOS;
    public GameObject currentTarget;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public CapsuleCollider range;
    //public GameObject currentTarget;
    //public EnemyInRange enemies;
    public bool lineOfSightBlocked;
    public float framesBetweenShots;
    public float shotFrameCycle;

    public float fullMagazine;
    float ammoRemaining;
    public float setReloadTime;
    float currentReload;
    bool ammoEmpty = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")  && transform.parent.gameObject.tag == "Player" || (other.CompareTag("Player") && transform.parent.gameObject.tag == "Enemy"))
        {
            targetsInRange.Add(other.gameObject);
            SetTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && transform.parent.gameObject.tag == "Player" || (other.CompareTag("Player") && transform.parent.gameObject.tag == "Enemy"))
        {
            targetsInRange.Remove(other.gameObject);
            SetTarget();
        }
    }

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

    void CheckForEnemy()
    {
        if (currentTarget != null)
        {
            CheckLOS();
        }
        else
        {
            
        }
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
                    //raycastHit.collider.gameObject.lay
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
                if(targetsInLOS[i] == null)
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

            currentTarget = targetsInLOS[indexOfMin];
            Shoot();

            Debug.Log(indexOfMin);
        }
    }

    void Shoot()
    {
        if (ammoEmpty == true)
        {
            return;
        }

        if (framesBetweenShots <= shotFrameCycle)
        {
            GameObject bullet;
            bullet = Instantiate(bulletPrefab, transform);
            bullet.transform.parent = null;
            bullet.GetComponent<Rigidbody>().velocity = ((currentTarget.transform.position - transform.position) * bulletSpeed);
            shotFrameCycle = 0;
            ammoRemaining--;
        }
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

    IEnumerator Reloading()
    {
        Debug.Log("Reloading");
        yield return new WaitForSeconds(currentReload);
        ammoRemaining = fullMagazine;
        ammoEmpty = false;
    }

    void Reload() //in case the coroutine does not work I will set up another method
    {
        Debug.Log("This is being accessed");

        if (currentReload > 0)
        {
            currentReload -= Time.deltaTime;
            Debug.Log(currentReload);
        }
        else if (currentReload <= 0)
        {
            currentReload = setReloadTime;
            ammoRemaining = fullMagazine;
            ammoEmpty = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentReload = setReloadTime;
        ammoRemaining = fullMagazine;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckForEnemy();
        SetTargetToClosest();
        CheckLOS();

        if (ammoRemaining <= 0)
        {
            ammoEmpty = true;
            Reload();
            //StartCoroutine(Reloading());
        }
    }

    private void FixedUpdate()
    {
        shotFrameCycle++;
    }
}