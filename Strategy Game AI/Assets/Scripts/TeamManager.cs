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
        
    }

    void TrainNewUnit()
    {

    }

    void TrainNewSoldier()
    {

    }
}