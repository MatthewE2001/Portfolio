using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject player;
    public float speed;

    Vector3 playerLocationAtShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        player = GameObject.Find("Player");
        playerLocationAtShot = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();

        if (Vector3.Distance(gameObject.transform.position, playerLocationAtShot) < 1.0f)
        {
            Destroy(gameObject);
        }
    }

    public void MoveTowardsPlayer()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, 
            playerLocationAtShot, speed);
    }
}
