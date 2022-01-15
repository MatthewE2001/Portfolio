using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{

    public void LoadNewScene(int buildindex)
    {
        PartyTracker.instance.NewScenePrep();
        SceneManager.LoadScene(buildindex);
        PartyTracker.instance.NewSceneLoaded();
    }

    public void LoadNextScene()
    {
        PartyTracker.instance.NewScenePrep();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PartyTracker.instance.NewSceneLoaded();
    }

    public void LoadNewScene(string name)
    {
        PartyTracker.instance.NewScenePrep();
        SceneManager.LoadScene(name);
        PartyTracker.instance.NewSceneLoaded();
    }

    public void LoadNewSceneNormal(int buildindex)
    {
        SceneManager.LoadScene(buildindex);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    LoadNewScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    LoadNewScene(SceneManager.GetActiveScene().buildIndex - 1);
        //}
    }
}
