using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int currentFood;
    int currentGold;
    int baseHealth; //might not need health of the base here i am not sure
    int currentUnitTrainingTime;
    int currentSoldierTrainingTime;

    bool locationChangeNeeded = true;

    // Start is called before the first frame update
    void Start()
    {
        currentFood = startingFood;
        currentGold = startingGold;

        SetAIMoveLocations();

        for (int i = 0; i < aiUnits.Length; i++)
        {
            aiUnits[i].GetComponent<MoveAI>().ChangeIsMoving();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (locationChangeNeeded == true)
        {
            for (int i = 0; i < aiUnits.Length; i++)
            {
                if (aiUnits[i].transform.position == new Vector3(0,0,0))
                {
                    aiUnits[i].GetComponent<MoveAI>().SetLocation(gameObject.transform.position);
                    aiUnits[i].GetComponent<MoveAI>().ChangeIsMoving();
                }
                
                if (aiUnits[i].transform.position == goldObject.transform.position)
                {
                    aiUnits[i].GetComponent<MoveAI>().SetLocation(gameObject.transform.position);
                    aiUnits[i].GetComponent<MoveAI>().ChangeIsMoving();
                }
                
                if (aiUnits[i].transform.position == gameObject.transform.position)
                {
                    aiUnits[i].GetComponent<MoveAI>().SetLocation(goldObject.transform.position);
                    aiUnits[i].GetComponent<MoveAI>().ChangeIsMoving();
                }
            }

            locationChangeNeeded = false;
        }
    }

    void TrainNewUnit()
    {

    }

    void TrainNewSoldier()
    {

    }

    void SetAIMoveLocations()
    {
        //for (int i = 0; i < aiUnits.Length; i++)
        //{
        //    aiUnits[i].GetComponent<MoveAI>().movePosition = goldObject.transform.position;
        //}
    }

    public void SetLocationReached()
    {
        locationChangeNeeded = true;
    }
}