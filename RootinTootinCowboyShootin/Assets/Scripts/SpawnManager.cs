using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Used strictly for debugging purposes or until a proper spawning system is made
public class SpawnManager : MonoBehaviour 
{
    public bool scenarioStarted = false;
    public bool waveClearState = false;
    public bool allWavesClear = false;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesRemainingText;

    public GameObject enemyPrefab;

    public List<GameObject> spawners;

    public List<Wave> waves;
    public int activeWave;
    public int enemyIndex;

    public List<GameObject> activeEnemies;

    public float timeBetweenSpawnsMin;
    public float timeBetweenSpawnsMax;

    public int clusterSizeMin;
    public int clusterSizeMax;
    
    public float nextSpawnCountdown;

    public float timeBetweenWavesInSeconds;
    public float nextWaveCountdown;

    public int enemyCap;
    public int enemyDeathsThisWave;

    void CheckInput()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Spawn(0, false);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Spawn(1, false);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Spawn(2, false);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    Spawn(3, false);
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            StartScenario();
        }
    }

    void UpdateUI()
    {
        if(!scenarioStarted)
        {
            waveText.text = "Press Space to Begin Combat";
            enemiesRemainingText.text = "";
        }
        else if(waveClearState)
        {
            waveText.text = "Next Wave:";
            enemiesRemainingText.text = nextWaveCountdown.ToString("F2");
        }
        else
        {
            int currentWave = activeWave + 1;
            waveText.text = "Wave: " + currentWave.ToString() + "/" + waves.Count;
            int remaining = waves[activeWave].enemiesToSpawn.Count - enemyDeathsThisWave;
            enemiesRemainingText.text = remaining.ToString() + " Enemies Remain";
        }

    }

    void Spawn(int spawnerIndex, bool random = true)
    {
        GameObject newEnemy;
        if(random)
        {
            newEnemy = Instantiate(enemyPrefab, spawners[(int)Random.Range(0, spawners.Count - 1)].transform);
        }
        else
        {
            newEnemy = Instantiate(enemyPrefab, spawners[spawnerIndex].transform);
        }
        newEnemy.transform.parent = null;
    }

    void Spawn()
    {
        int numToSpawn = Random.Range(clusterSizeMin, clusterSizeMax);
        int spawnerToUse = (int)Random.Range(0, spawners.Count - 1);

        for(int i = 0; i < numToSpawn; i++)
        {
            GameObject newEnemy;
            newEnemy = Instantiate(waves[activeWave].enemiesToSpawn[enemyIndex], spawners[spawnerToUse].transform);

            newEnemy.transform.parent = null;
            newEnemy.transform.localScale = Vector3.one; //theres a bug that sets scale to 2.5 sometimes? no idea why but this fixes it
            nextSpawnCountdown = Random.Range(timeBetweenSpawnsMin, timeBetweenSpawnsMax);
            enemyIndex++;
            if(enemyIndex >= waves[activeWave].enemiesToSpawn.Count)
            {
                return;
            }
        }
    }

    void ActionLoop()
    {
        if (waveClearState)
        {
            nextWaveCountdown -= Time.deltaTime;
            if(nextWaveCountdown <= 0f)
            {
                waveClearState = false;
            }
        }
        else if (nextSpawnCountdown <= 0f)
        {
            if(enemyIndex < waves[activeWave].enemiesToSpawn.Count)
            {
                if(GameObject.FindGameObjectsWithTag("Enemy").Length < enemyCap)
                {
                    Spawn();
                }
            }
            else if(enemyDeathsThisWave >= waves[activeWave].enemiesToSpawn.Count) //next wave start
            {
                if(activeWave < waves.Count-1)
                {
                    activeWave++;
                    Debug.Log("Wave " + activeWave);
                    waveClearState = true;
                    nextWaveCountdown = timeBetweenWavesInSeconds;
                    enemyDeathsThisWave = 0;
                    enemyIndex = 0;
                }
                else
                {
                    Debug.Log("Waves Complete");
                    allWavesClear = true;
                    //end scenario
                }
            }
        }
    }

    void StartScenario()
    {
        if(!scenarioStarted)
        {
            scenarioStarted = true;
            Spawn();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawners.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawner"));
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateUI();
        if(scenarioStarted)
        {
            
            nextSpawnCountdown -= Time.deltaTime;
            ActionLoop();
        }
    }
}
