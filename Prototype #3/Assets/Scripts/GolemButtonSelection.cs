using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GolemButtonSelection : MonoBehaviour
{
    public int chooseGolem = 0;
    public string sceneTransition;
    static int i = 0;
    //public bool refreshLoad = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.transform.gameObject); //idk if this might work we will see
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartExplorationGolem()
    {
        //set golem active to 1 and then launch into game scene
        chooseGolem = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        //RefreshLoadChange();

        
         SceneManager.LoadScene(sceneTransition);
       
        //Scene tmpScene = new Scene();
        //tmpScene.name = sceneTransition;
        //SceneManager.SetActiveScene(tmpScene); //setactivescene does not seem to work how I expected
        //i++;
        
        //GameObject.Find("GameManager").GetComponent<ChangeGolems>().SetGolemActive();
    }

    public void StartAttackGolem()
    {
        //set golem active to 2 and then launch into game scene
        chooseGolem = 2;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        //RefreshLoadChange();
        SceneManager.LoadScene(sceneTransition);
        //GameObject.Find("GameManager").GetComponent<ChangeGolems>().SetGolemActive();
    }

    public void StartCartographyGolem()
    {
        //set golem active to 3 and then launch into game scene
        chooseGolem = 3;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        //RefreshLoadChange();
        SceneManager.LoadScene(sceneTransition);
        //GameObject.Find("GameManager").GetComponent<ChangeGolems>().SetGolemActive();
    }

    public int ReturnGolemValue()
    {
        //Debug.Log("SEND THE FUCKING VALUE");
        return chooseGolem;
    }

    //public void RefreshLoadChange()
    //{
    //    if (refreshLoad == true)
    //    {
    //        refreshLoad = false;
    //    }

    //    if (refreshLoad == false)
    //    {
    //        refreshLoad = true;
    //    }
    //}
}
