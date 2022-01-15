using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatSceneScript : MonoBehaviour
{
    public string sceneName;
    public int maxDeaths = 3;
    public int currentDeaths = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckLoseCondition();
    }

    void CheckLoseCondition()
    {
        if (currentDeaths == maxDeaths)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
