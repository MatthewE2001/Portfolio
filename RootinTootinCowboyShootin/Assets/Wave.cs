using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public List<GameObject> enemiesToSpawn;
    public List<GameObject> enemiesAlreadySpawned; //maybe delete this
    public int totalEnemies;
    public int enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        totalEnemies = enemiesToSpawn.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
