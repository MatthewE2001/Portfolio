using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicMenu : MonoBehaviour
{
    public string sceneTransition;
    void Start()
    {
        //DontDestroyOnLoad(this.transform.gameObject); //idk if this might work we will see
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Scene Transition
    public void MoveScene()
    {
        SceneManager.LoadScene(sceneTransition);
    }

    // Quit Game
    public void QuitGame()
    {
        Application.Quit();
    }

    //Restart Game
    public void RestartGame()
    {

    }
}
