using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for projectiles meant to hit the player
//Not used anymore, bulletScript serves same function
public class EnemyBulletScript : MonoBehaviour
{
    public float framesBeforeDestroy;
    public float currentFrame;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterHealthScript>().Hit(20f, gameObject.transform.position);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (framesBeforeDestroy <= currentFrame)
        {
            Destroy(gameObject);
        }
        currentFrame++;
    }
}