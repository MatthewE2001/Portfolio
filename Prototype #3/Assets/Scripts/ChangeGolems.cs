using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeGolems : MonoBehaviour
{
    [Tooltip("1 exploration, 2 attack, 3 cartography")]
    public int golemActive = 1; //1 exploration, 2 attack, 3 cartography
    bool golemChanged = false;

    // Inspector Editable Values
    public float explorationGolemSpeed = 6.0f;
    public int explorationGolemHealth = 10;
    public int explorationGolemStrength = 3;
    public float attackGolemSpeed = 3.0f;
    public int attackGolemHealth = 15;
    public int attackGolemStrength = 6; //just a random value I picked here
    public float cartographyGolemSpeed = 9.0f;
    public int cartographyGolemHealth = 5;
    public int cartographyGolemStrength = 1;
    
    // Materials
    public Material explorationMaterial;
    public Material attackMaterial;
    public Material cartographyMaterial;

    // Text
    public Text playerHealth;
    public Text golemText;


    //these values will change depending on the golem active
    float speed = 6.0f;
    int health = 10;
    public Material currentMaterial;
    float attackStrength = 4;
    bool selectionChange = false;

    // Start is called before the first frame update
    void Start()
    {
        SetGolemText();
        SetGolemActive();
    }

    private void Awake()
    {
        //set the golem active type here from the selection scene
        SetGolemActive();
    }

    // Update is called once per frame
    void Update()
    {
        SwapGolem();
        UpdateHealthText();
        CheckPlayerSpeed();
        
        golemActive = GameObject.Find("GolemSceneManager").GetComponent<GolemButtonSelection>().ReturnGolemValue();
        //Debug.Log(golemActive);
    }

    void SwapGolem()
    {
        if (golemActive == 1)
        {
            //golemActive = 1;
            speed = explorationGolemSpeed;
            currentMaterial = explorationMaterial;
            GameObject.Find("Player").GetComponent<Renderer>().material = currentMaterial;
            attackStrength = explorationGolemStrength;
            golemChanged = false;
            SetGolemText();
            
            if (selectionChange == true)
            {
                health = explorationGolemHealth;
                GameObject.Find("Player").GetComponent<PlayerHealth>().health = health;
                selectionChange = false;
                //GameObject.Find("GolemSceneManager").GetComponent<GolemButtonSelection>().RefreshLoadChange();
            }

            //ResetGolemChanged();
        }
        
        if (golemActive == 2)
        {
            //golemActive = 2;
            speed = attackGolemSpeed;
            currentMaterial = attackMaterial;
            GameObject.Find("Player").GetComponent<Renderer>().material = currentMaterial;
            attackStrength = attackGolemStrength;
            golemChanged = false;
            SetGolemText();

            if (selectionChange == true)
            {
                health = attackGolemHealth;
                GameObject.Find("Player").GetComponent<PlayerHealth>().health = health;
                selectionChange = false;
                //GameObject.Find("GolemSceneManager").GetComponent<GolemButtonSelection>().RefreshLoadChange();
                //Debug.Log("Is this always going?");
            }

            //ResetGolemChanged();
        }

        if (golemActive == 3)
        {
            //golemActive = 3;
            speed = cartographyGolemSpeed;
            currentMaterial = cartographyMaterial;
            GameObject.Find("Player").GetComponent<Renderer>().material = currentMaterial;
            attackStrength = cartographyGolemStrength;
            golemChanged = false;
            SetGolemText();
            
            
            if (selectionChange == true)
            {
                health = cartographyGolemHealth;
                GameObject.Find("Player").GetComponent<PlayerHealth>().health = health;
                selectionChange = false;
                //GameObject.Find("GolemSceneManager").GetComponent<GolemButtonSelection>().RefreshLoadChange();
            }

            //ResetGolemChanged();
        }
    }

    public void UpdateHealthText()
    {
        playerHealth.text = "Player Health is: " + GameObject.Find("Player").GetComponent<PlayerHealth>().health.ToString();
    }

    public void CheckPlayerSpeed()
    {
        if (golemActive == 1)
        {
            speed = explorationGolemSpeed;
        }
        else if (golemActive == 2)
        {
            speed = attackGolemSpeed;
        }
        else if (golemActive == 3)
        {
            speed = cartographyGolemSpeed;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetDamage()
    {
        return attackStrength;
    }

    public int GetActiveGolem()
    {
        return golemActive;
    }

    public void ResetGolemChanged()
    {
        golemChanged = false;
        //Debug.Log("Golem Status Changed");
    }

    public bool GolemStatus()
    {
        return golemChanged;
    }

    public void ChangeAttackGolemStats(float speed, int attack)
    {
        attackGolemSpeed = speed;
        attackGolemStrength = attack;
    }

    public void SetGolemText()
    {
        if (golemActive == 1)
        {
            golemText.text = "Current Golem is Exploration";
        }
        else if (golemActive == 2)
        {
            golemText.text = "Current Golem is Attack";
        }
        else if (golemActive == 3)
        {
            golemText.text = "Current Golem is Cartography";
        }
    }

    public void SetGolemActive()
    {
        golemActive = GameObject.Find("GolemSceneManager").GetComponent<GolemButtonSelection>().ReturnGolemValue();
        Debug.Log("Golem Value changed");
        Debug.Log(golemActive);
        selectionChange = true;
    }
}
