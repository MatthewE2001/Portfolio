using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button resumeButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMainMenu()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0; //pause the game

            //activate/make appear the buttons for the main menu
                //resume
                //options
                //quit
        }

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;

            //deactivate main menu buttons
        }
    }
}