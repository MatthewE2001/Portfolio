using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button exitButton;
    public Button resumeButton;
    public bool paused;

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (!paused)
        {
            Time.timeScale = 0.0f;
            paused = true;
            exitButton.gameObject.SetActive(true);
            resumeButton.gameObject.SetActive(true);
            
        }
        else
        {
            Time.timeScale = 1.0f;
            paused = false;
            exitButton.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if(!paused)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
        //Debug.Log(Time.timeScale);
    }
}
