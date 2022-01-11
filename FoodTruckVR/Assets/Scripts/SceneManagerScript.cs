using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private static SceneManagerScript instance;
    public static SceneManagerScript Instance { get { return instance; } }

    public enum Levels
    {
        MainMenu,
        PlanetOne,
        NumOflevels
    };

    public GameObject mainMenuLoader;
    public GameObject verticalSliceLoader;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void LoadLevel(Levels level)
    {
        switch (level)
        {
            case Levels.MainMenu:
                {
                    Instantiate(mainMenuLoader);
                    break;
                }
            case Levels.PlanetOne:
                {
                    Instantiate(verticalSliceLoader);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
