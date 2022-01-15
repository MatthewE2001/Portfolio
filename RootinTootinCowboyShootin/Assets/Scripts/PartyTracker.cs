using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PartyTracker : MonoBehaviour
{
    public static PartyTracker instance = null;

    public GameObject[] playerUnits;
    public List<GameObject> playerSpawns;
    private IEnumerator waitForLoadRoutine;

    public int lawmen;
    public int cowboys;
    public bool checkForUnits;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Debug.Log("New Scene loaded");
            Destroy(gameObject);
        }
    }

    public void AddCowboy()
    {
        cowboys++;
    }

    public void AddLawman()
    {
        lawmen++;
    }

    IEnumerator WaitForSceneLoad()
    {
        while (SceneManager.GetSceneByBuildIndex(2).isLoaded == false)
        {
            Debug.Log("Loading");
            yield return new WaitForSeconds(0);
        }
        NewSceneLoaded();
        Debug.Log("Loaded!");
    }

    public void NewScenePrep()
    {
        checkForUnits = false;
        for(int i = 0; i < playerUnits.Length; i++)
        {
            playerUnits[i].transform.parent = transform;
        }
        waitForLoadRoutine = WaitForSceneLoad();
        StartCoroutine(waitForLoadRoutine);
        
        //SceneManager.LoadScene();
    }

    bool FindSpawnPoints()
    {
        playerSpawns.Clear();
        playerSpawns.AddRange(GameObject.FindGameObjectsWithTag("Spawner"));
        if(playerSpawns.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    public void NewSceneLoaded()
    {

        if (FindSpawnPoints())
        {
            Debug.Log("Spawn points found");
            PlaceUnits();
            
        }
    }
    


    void PlaceUnits()
    {
        for(int i = 0; i < playerUnits.Length; i++)
        {
            playerUnits[i].transform.parent = null;
            playerUnits[i].GetComponent<NavMeshAgent>().enabled = false;
            playerUnits[i].transform.position = playerSpawns[i].transform.position;
            playerUnits[i].GetComponent<NavMeshAgent>().enabled = true;
            playerUnits[i].GetComponent<PlayerUnitMovement>().UpdateCam();
        }
        checkForUnits = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerSpawns = new List<GameObject>();
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(checkForUnits) //ensures the tracker doesnt check for units when the party has not loaded into the scene for whatever reason
        {
            playerUnits = GameObject.FindGameObjectsWithTag("Player");
        }
    }
}
