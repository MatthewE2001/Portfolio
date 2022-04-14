using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(currentHealth);
        currentHealth -= 10;
        Destroy(collision.collider.gameObject);
    }
}