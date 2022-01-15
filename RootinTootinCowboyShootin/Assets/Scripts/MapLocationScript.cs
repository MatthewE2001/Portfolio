using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLocationScript : MonoBehaviour
{
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DanSceneChange()
    {
        SceneManager.LoadScene(1);
    }

    public void NickSceneChange()
    {
        SceneManager.LoadScene(2);
    }
}
