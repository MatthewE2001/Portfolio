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
    public GameObject unitToTrain;
    //public GameObject soldierToTrain;
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

        for (int i = 0; i < aiUnits.Length; i++)
        {
            aiUnits[i].GetComponent<MoveAI>().SetLocation(foodObject.transform.position);
        }
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
                //just having it default to food for now but could also have it pick at random
                aiUnits[i].GetComponent<MoveAI>().SetLocation(foodObject.transform.position);
                aiUnits[i].GetComponent<MoveAI>().ChangeHasAction(true);
            }

            if (aiUnits[i].GetComponent<Inventory>().GetInventoryStatus() == true)
            {
                aiUnits[i].GetComponent<MoveAI>().SetLocation(gameObject.transform.position);
            }

            if (Vector3.Distance(aiUnits[i].transform.position, foodObject.transform.position) < 3.0f)
            {
                aiUnits[i].GetComponent<Inventory>().ChangeCollectionStatus(true);
            }

            if (Vector3.Distance(aiUnits[i].transform.position, goldObject.transform.position) < 3.0f)
            {
                aiUnits[i].GetComponent<Inventory>().ChangeCollectionStatus(true);
            }

            if (Vector3.Distance(aiUnits[i].transform.position, gameObject.transform.position) < 3.0f)
            {
                if (aiUnits[i].GetComponent<Inventory>().resourceType == ResourceTypes.Food)
                {
                    currentFood += 10;
                    Debug.Log(gameObject.name + " " + currentFood);
                }

                if (aiUnits[i].GetComponent<Inventory>().resourceType == ResourceTypes.Gold)
                {
                    currentGold += 10;
                    Debug.Log(gameObject.name + " " + currentFood);
                }

                aiUnits[i].GetComponent<MoveAI>().ChangeHasAction(false);
                aiUnits[i].GetComponent<Inventory>().ChangeInventoryStatus();  
                
                if (currentFood > foodForUnit)
                {
                    Debug.Log("Training Unit");
                    currentFood -= foodForUnit;
                    TrainNewUnit();
                }

                if (currentGold > goldForSoldier)
                {
                    Debug.Log("Training Soldier");
                    currentGold -= goldForSoldier;
                    TrainNewSoldier();
                }
            }
        }
    }

    void TrainNewUnit()
    {
        if (currentFood >= foodForUnit)
        {
            //train up unit
            
        }
    }

    void TrainNewSoldier()
    {
        if (currentGold >= goldForSoldier)
        {
            //train up soldier
            //Instantiate(
            //take away amount of gold
        }
    }

    void SetAIMoveLocations()
    {
        
    }

    public void ChangeAIMoveSpeed()
    {
        for (int i = 0; i < aiUnits.Length; i++)
        {
            aiUnits[i].GetComponent<MoveAI>().ChangeMoveSpeed(moveSpeedSlider.value);
        }
    }

    public int GetFoodCount()
    {
        return currentFood;
    }

    public int GetGoldCount()
    {
        return currentGold;
    }
}