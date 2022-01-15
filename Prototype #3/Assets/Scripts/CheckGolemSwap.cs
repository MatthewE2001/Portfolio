using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGolemSwap : MonoBehaviour
{
    /*
     * The reason on wnhy this script exists is to better facilitate the golem swapping for the proof of concept.
     * What happened was that both the player's movement and player's health scripts were polling the GameManager at teh same time.
     * This lead to the value of if the golem swap was true or not being constantly changed from true to false. Kinda like a "pointless machine"
     * This prevented the health and player values from actually changing.
     * By having one universal script that changes the values and seperate scripts which use those values (ie movement and health) this bug becomes squahshed.
     * 
     * - Nicholas Adams, 2/15/2020 12:24 pm.
     */
    
    // Update is called once per frame
    void Update()
    {
        GolemSwap();
    }

    // Universal Golem Swap Code for the Player
    void GolemSwap()
    {
        if (GameObject.Find("GameManager").GetComponent<ChangeGolems>().GolemStatus() == true)
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().health = GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetHealth(); // Set the new the Health
            GameObject.Find("Player").GetComponent<PlayerMovement>().moveSpeed = GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetSpeed(); // Set the new Speed
            GameObject.Find("Player").GetComponent<Renderer>().material = GameObject.Find("GameManager").GetComponent<ChangeGolems>().currentMaterial; // Set the new material color
            //Debug.Log("Health: " + GameObject.Find("Player").GetComponent<PlayerHealth>().health); // Debug Log Health
            //Debug.Log("Speed: " + GameObject.Find("Player").GetComponent<PlayerMovement>().moveSpeed); // Debug Log Speed
            GameObject.Find("GameManager").GetComponent<ChangeGolems>().ResetGolemChanged(); // Reset the golemChanged boolean
        }
    }

}
