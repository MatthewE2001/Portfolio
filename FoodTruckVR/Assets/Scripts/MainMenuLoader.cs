using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManagerScript.Instance.LoadLevel(SceneManagerScript.Levels.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
