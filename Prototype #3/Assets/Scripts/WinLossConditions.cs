using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLossConditions : MonoBehaviour
{
    //public Text winMessage; //these texts might need to be in another script
    //public Text lossMessage;
    int lifeCountHold;
    // Start is called before the first frame update
    void Start()
    {
        lifeCountHold = GameObject.Find("Player").GetComponent<PlayerRespawn>().GetLifeAmount();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCountHold > GameObject.Find("Player").GetComponent<PlayerRespawn>().GetLifeAmount())
        {
            lifeCountHold = GameObject.Find("Player").GetComponent<PlayerRespawn>().GetLifeAmount();
        }

        if(lifeCountHold <= 0)
        {
            //activate the loss screen
            GameOverLoss();
        }
    }

    private void OnTriggerEnter(Collider other) //idk how else to work with the player hitting the end location?
    {
        if (other.tag == "Player")
        {
            GameOverWin();
        }
    }

    public void GameOverLoss()
    {
        SceneManager.LoadScene("LossScene");
    }

    public void GameOverWin()
    {
        SceneManager.LoadScene("WinScene");
    }
}
