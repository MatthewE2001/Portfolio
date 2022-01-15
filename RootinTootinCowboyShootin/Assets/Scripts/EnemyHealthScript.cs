using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public float maxHealth = 70.0f;
    public float currentHealth;
    public GameObject enemyPrefab;

    VictorySceneScript victory;

    public float accuracyBuff = 1.0f;
    public float dexterityBuff = 1.0f;

    public void Hit(float damange = 20.0f)
    {
        currentHealth -= damange;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        victory = GameObject.Find("GameObject").GetComponent<VictorySceneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        if (currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;
            victory.currentDeaths += 1;
            Destroy(this.gameObject);
        }
    }
}
