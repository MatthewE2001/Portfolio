using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject player;
    public float speed;

    Vector3 playerLocationAtShot;
    Vector3 startMoveLocation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerLocationAtShot = player.transform.position;
        startMoveLocation = gameObject.transform.position;
    }

    void Awake()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        startMoveLocation = gameObject.transform.position;
        MoveTowardsPlayer();

        if (Vector3.Distance(gameObject.transform.position, playerLocationAtShot) < 1.0f)
        {
            Destroy(gameObject);
        }
    }

    public void MoveTowardsPlayer()
    {
        gameObject.transform.position = Vector3.MoveTowards(startMoveLocation, 
            playerLocationAtShot, speed);
    }
}
