using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckGolemSwap();
    }

    public void DamagePlayer(int attack)
    {
        health -= attack;
        //GameObject.Find("GameManager").GetComponent<ChangeGolems>().UpdateHealthText();
        Debug.Log("Damage Done");
    }

    void CheckGolemSwap()
    {
        if (GameObject.Find("GameManager").GetComponent<ChangeGolems>().GolemStatus() == true)
        {
            health = GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetHealth();
            //Debug.Log(health);
            GameObject.Find("GameManager").GetComponent<ChangeGolems>().ResetGolemChanged();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void ResetHealth()
    {
        health = GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetHealth();
    }
}
