using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for any kind of projectile meant to hit an enemy
//Still running on frames for time logic, needs to be updated
public class BulletScript : MonoBehaviour
{
    //public float framesBeforeDestroy;
    public float timeBeforeDestroyInSeconds;
    public float lifetimeInSeconds;
    //public float currentFrame;
    public float damage;

    public int penetrationPower; //1 = bullet can penetrate 1 enemy, 2 = penetrate 2 enemies, etc
    private int unitsPenetrated;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && gameObject.layer == 12)
        {
            collision.gameObject.GetComponent<CharacterHealthScript>().Hit(damage, gameObject.transform.position);
            unitsPenetrated++;
            
            if(unitsPenetrated > penetrationPower)
            Destroy(gameObject);

        }
        else if(collision.gameObject.CompareTag("Player") && gameObject.layer == 13)
        {
            collision.gameObject.GetComponent<CharacterHealthScript>().Hit(damage, gameObject.transform.position);
            unitsPenetrated++;

            if(unitsPenetrated > penetrationPower)
            Destroy(gameObject);
        }
        else if(!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Enemy"))//bullet hit something other than an enemy or player
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && gameObject.layer == 12)
        {
            collision.gameObject.GetComponent<CharacterHealthScript>().Hit(damage, gameObject.transform.position);
            unitsPenetrated++;

            if (unitsPenetrated > penetrationPower)
                Destroy(gameObject);

        }
        else if (collision.gameObject.CompareTag("Player") && gameObject.layer == 13)
        {
            collision.gameObject.GetComponent<CharacterHealthScript>().Hit(damage, gameObject.transform.position);
            unitsPenetrated++;

            if (unitsPenetrated > penetrationPower)
                Destroy(gameObject);
        }
        else //hits something that isnt a player/enemy unit
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform.localScale = Vector3.one;
        unitsPenetrated = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(timeBeforeDestroyInSeconds <= lifetimeInSeconds)
        {
            Destroy(gameObject);
        }
        lifetimeInSeconds += Time.deltaTime;
    }
}
