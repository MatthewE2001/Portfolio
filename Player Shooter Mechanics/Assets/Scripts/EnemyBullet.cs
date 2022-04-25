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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTowardsPlayer()
    {
        playerLocationAtShot = player.transform.position;

        //Vector3.MoveTowards
    }
}
