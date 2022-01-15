using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float accuracyError; //1 = 90 degree margin of error in both directions, .5 = 45 degrees, etc. Does not affect Y value.
    public float accuracyPercent;
    public CharacterHealthScript unitStats;
    public AttackUnitInRange unitInRange;

    private IEnumerator cooldownRoutine;
    public float cooldownInSeconds;
    public bool attackReady;
    public bool overrideRange;
    public float newRange;

    public float fullMagazine;
    public float ammoRemaining;
    public float setReloadTime;
    public float currentReload;
    public float bulletDamage;
    public bool ammoEmpty = false;

    
    // Start is called before the first frame update
    void Start()
    {
        cooldownRoutine = StartCooldown();
        StartCoroutine(cooldownRoutine);
        unitStats = transform.parent.GetComponent<CharacterHealthScript>();
        currentReload = setReloadTime;
        ammoRemaining = fullMagazine;

        if (overrideRange)
        {
            transform.parent.GetComponent<AttackUnitInRange>().range = newRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ammoRemaining <= 0)
        {
            ammoEmpty = true;
            Reload();
            Debug.Log("This actually happens");
        }

        UpdateReloadUI();
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (gameObject.tag == "Enemy")
    //        Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.15f);
    //    else
    //        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.15f);

    //    if (overrideRange)
    //        Gizmos.DrawSphere(transform.position, newRange);
    //    else
    //        Gizmos.DrawSphere(transform.position, GetComponent<AttackUnitInRange>().range);
    //}

    public void FireWeapon()
    {
        ChangeAnimationTrigger(true);

        if (ammoEmpty == true) //prevents shooting during reload time
        {
            return;
        }

        if (attackReady)
        {
            Debug.Log("MeleePew");
            GameObject bullet;
            bullet = Instantiate(bulletPrefab, transform);
            bullet.transform.parent = null;
            Vector3 inaccuracyVector;
            float shotAccuracyError = accuracyError - ((accuracyError * unitStats.accuracyBuff) - accuracyError); //checks the buff status of the current unit, if not currently buffed it equals 1.0
            
            if (Random.Range(0.0f, 100.0f) >= accuracyPercent)
            {
                //add inaccuracy error
                inaccuracyVector = new Vector3(Random.Range(shotAccuracyError * -1, shotAccuracyError), 0, Random.Range(shotAccuracyError * -1, shotAccuracyError));
            }
            else
            {
                //set inaccuracy to zero
                inaccuracyVector = Vector3.zero;
            }
            
            bullet.GetComponent<Rigidbody>().velocity = (Vector3.Normalize(GetComponent<AttackUnitInRange>().currentTarget.transform.position - transform.position
                                                         + inaccuracyVector) //set to zero if inaccuracy check fails
                                                         * bulletSpeed);
            
            bullet.GetComponent<BulletScript>().damage = bulletDamage;
            ammoRemaining--;
            Debug.Log(ammoRemaining);

            attackReady = false; //runs IEnumerator cooldown routine
        }

        //ChangeAnimationTrigger(false);
    }
    

    IEnumerator StartCooldown()
    {
        while (true)
        {
            if (!attackReady)
            {
                float cooldown = cooldownInSeconds - ((cooldownInSeconds * unitStats.dexterityBuff) - cooldownInSeconds);
                yield return new WaitForSeconds(cooldown);
                attackReady = true;
            }

            yield return new WaitForSeconds(0);
        }
    }

    //unused
    //IEnumerator Reloading() 
    //{
    //    Debug.Log("Reloading");
    //    yield return new WaitForSeconds(currentReload);
    //    ammoRemaining = fullMagazine;
    //    ammoEmpty = false;
    //}

    void UpdateReloadUI() //Temporary solution to visualize reloading
    {
        //if (ammoEmpty)
        //{
        //    transform.parent.gameObject.GetComponentInChildren<ReloadTextMesh>().gameObject.GetComponent<TextMesh>().color = Color.magenta;
        //    transform.parent.gameObject.GetComponentInChildren<ReloadTextMesh>().gameObject.GetComponent<TextMesh>().text = currentReload.ToString("F2") + "...";
        //}
        //else
        //{
        //    transform.parent.gameObject.GetComponentInChildren<ReloadTextMesh>().gameObject.GetComponent<TextMesh>().color = Color.cyan;
        //    transform.parent.gameObject.GetComponentInChildren<ReloadTextMesh>().gameObject.GetComponent<TextMesh>().text = ammoRemaining.ToString();
        //}
    }

    void Reload() //in case the coroutine does not work I will set up another method
    {
        if (currentReload > 0)
        {
            currentReload -= Time.deltaTime;
            //Debug.Log(currentReload);
        }
        else if (currentReload <= 0)
        {
            currentReload = unitStats.dexterityBuff - ((setReloadTime * unitStats.dexterityBuff) - setReloadTime);
            ammoRemaining = fullMagazine;
            ammoEmpty = false;
        }
    }

    public void ChangeAnimationTrigger(bool change)
    {
        gameObject.GetComponent<Animator>().SetBool(0, change); //idk if this should work for this?
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ChangeAnimationTrigger(true);
            collision.gameObject.GetComponent<CharacterHealthScript>().Hit(10, gameObject.transform.position);
        }
    }

    public void AttackPlayer()
    {
        
    }
}
