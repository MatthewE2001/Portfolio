using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text healthText;
    public GameObject player;

    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = player.GetComponent<PlayerHealth>().GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        health = player.GetComponent<PlayerHealth>().GetHealth();

        healthText.text = "Player Health: " + health;
    }
}
