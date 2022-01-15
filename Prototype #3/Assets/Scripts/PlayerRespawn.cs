using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    public Transform spawnPoint;
    bool playerHealthEmpty = false;
    public int lifeCount = 5;
    //public string sceneTransition
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = spawnPoint.position;
    }

    private void Awake()
    {
        this.transform.position = spawnPoint.position; //I have this repeating in case loading causes differences
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerHealth();

        if (playerHealthEmpty == true)
        {
            RecreatePlayer();
            playerHealthEmpty = false;
        }
    }

    public void RecreatePlayer()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Destroy(GameObject.Find("GolemSceneManager"));
        SceneManager.LoadScene("FigureSelection"); // Edited to be the correct scene name - Nick
        this.transform.position = spawnPoint.position;
        lifeCount--;
    }

    public void CheckPlayerHealth()
    {
        if (this.GetComponent<PlayerHealth>().GetHealth() <= 0)
        {
            playerHealthEmpty = true;
            this.GetComponent<PlayerHealth>().ResetHealth();
        }
    }

    public int GetLifeAmount()
    {
        return lifeCount;
    }
}
