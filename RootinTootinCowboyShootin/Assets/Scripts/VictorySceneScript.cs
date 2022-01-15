using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictorySceneScript : MonoBehaviour
{
    public string sceneName;
    public int maxDeaths = 10;
    public int currentDeaths = 0;
    public GameObject winButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LoseCondition();
        WinCondition();
    }

    void LoseCondition()
    {
        GameObject[] cowboys = GameObject.FindGameObjectsWithTag("Player");
        if(cowboys.Length == 0)
        {
            SceneManager.LoadScene("Defeat_Scene");
        }
    }

    void WinCondition()
    {
        if(GameObject.FindObjectOfType<SpawnManager>().allWavesClear)
        {
            Debug.Log("AllWavesClear");
            //GameObject.FindObjectOfType<SceneLoader>().LoadNewScene("Victory_Scene");
            winButton.SetActive(true);
        }
    }

}
