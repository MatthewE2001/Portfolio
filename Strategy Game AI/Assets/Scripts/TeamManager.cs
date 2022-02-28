using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AIStates
{ 
    ResourceCollecting,
    Moving
}

public class TeamManager : MonoBehaviour
{
    public GameObject[] aiUnits;

    public int foodForUnit;
    public int unitTrainingTime;
    public int goldForSoldier;
    public int soldierTrainingTime;
    public int startingFood;
    public int startingGold;

    public GameObject goldObject;
    public GameObject foodObject;
    public Slider moveSpeedSlider;

    int currentFood;
    int currentGold;
    int baseHealth; //might not need health of the base here i am not sure
    int currentUnitTrainingTime;
    int currentSoldierTrainingTime;

    // Start is called before the first frame update
    void Start()
    {
        currentFood = startingFood;
        currentGold = startingGold;
    }

    // Update is called once per frame
    void Update()
    {    
        for (int i = 0; i < aiUnits.Length; i++)
        {
            if (currentGold < currentFood && aiUnits[i].GetComponent<MoveAI>().GetHasAction() == false)
            {
                aiUnits[i].GetComponent<MoveAI>().SetLocation(goldObject.transform.position);
                aiUnits[i].GetComponent<MoveAI>().ChangeHasAction(true);
            }

            if (currentFood < currentGold && aiUnits[i].GetComponent<MoveAI>().GetHasAction() == false)
            {
                aiUnits[i].GetComponent<MoveAI>().SetLocation(foodObject.transform.position);
                aiUnits[i].GetComponent<MoveAI>().ChangeHasAction(true);
            }

            if (currentFood == currentGold && aiUnits[i].GetComponent<MoveAI>().GetHasAction() == false)
            {
                //then maybe have it pick at random or just default to food? 
            }
        }

        SetAIMoveLocations();
    }

    void TrainNewUnit()
    {

    }

    void TrainNewSoldier()
    {

    }

    void SetAIMoveLocations()
    {
        for (int i = 0; i < aiUnits.Length; i++)
        {
            if (Vector3.Distance(aiUnits[i].transform.position, goldObject.transform.position) < 2.0f)
            {
                aiUnits[i].GetComponent<MoveAI>().SetLocation(gameObject.transform.position);
            }

            if (Vector3.Distance(aiUnits[i].transform.position, foodObject.transform.position) < 2.0f)
            {
                aiUnits[i].GetComponent<MoveAI>().SetLocation(gameObject.transform.position);
            }

            if (Vector3.Distance(aiUnits[i].transform.position, gameObject.transform.position) < 3.0f)
            {
                aiUnits[i].GetComponent<MoveAI>().ChangeHasAction(false);
            }
        }
    }

    public void ChangeAIMoveSpeed()
    {
        for (int i = 0; i < aiUnits.Length; i++)
        {
            aiUnits[i].GetComponent<MoveAI>().ChangeMoveSpeed(moveSpeedSlider.value);
        }
    }
}