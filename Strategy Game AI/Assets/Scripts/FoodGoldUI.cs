using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodGoldUI : MonoBehaviour
{
    public Text foodText;
    public Text goldText;

    public GameObject teamManager;

    int foodCount;
    int goldCount;

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        foodCount = teamManager.GetComponent<TeamManager>().GetFoodCount();
        goldCount = teamManager.GetComponent<TeamManager>().GetGoldCount();

        UpdateFoodText();
        UpdateGoldText();
    }

    void UpdateFoodText()
    {
        if (teamManager == GameObject.Find("Team1Manager"))
        {
            foodText.text = "Team 1 Food: " + foodCount.ToString();
        }
        
        if (teamManager == GameObject.Find("Team2Manager"))
        {
            foodText.text = "Team 2 Food: " + foodCount.ToString();
        }
    }

    void UpdateGoldText()
    {
        if (teamManager == GameObject.Find("Team1Manager"))
        {
            goldText.text = "Team 1 Gold: " + goldCount.ToString();
        }

        if (teamManager == GameObject.Find("Team2Manager"))
        {
            goldText.text = "Team 2 Gold: " + goldCount.ToString();
        }
    }
}
