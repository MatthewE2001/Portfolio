using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;
    public Vector3 distance = Vector3.zero;
    public int attackStrength = 3; //maybe should be an int?
    public float moveSpeed = 5.0f; // Added by Nick
    bool playerClose = false;
    bool moveBack = false;
    public float minDistance = 5.0f;

    public int enemyHealth = 5; //idk just as a random value
    public float coolDown = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveBack == true)
        {
            coolDown -= Time.deltaTime;
            Debug.Log("Enemy Cooldown: " + coolDown);

            if (coolDown <= 0.0f)
            {
                coolDown = 1.0f;
                moveBack = false;
            }
        }

        CheckDistance();

        if (playerClose == true && moveBack == false)
        {
            AttackPlayer();
        }
    }

    void CheckDistance()
    {
        distance = this.transform.position - player.transform.position;

        if (distance.x + distance.y + distance.z < minDistance && distance.x + distance.y + distance.z > -minDistance)
        {
            playerClose = true;
        }
    }

    void AttackPlayer()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, Time.deltaTime * moveSpeed);
    }
    
    
    // On Trigger Enter instead of OnCollisionEnter
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
            
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().DamagePlayer(attackStrength);
            playerClose = false;
            moveBack = true;
            Debug.Log("Uh");
        }
    }

    public void LowerEnemyHealth(float damage)
    {
        enemyHealth -= (int)damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    //not in use currently
    void Retreat()
    {
        //I wanna try and knock the enemy away a bit  
        Vector3 tmp = new Vector3(-player.transform.position.x, 0, -player.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, tmp, 1f);
        Debug.Log("Ahh A Player Run Away!");
    }
}
