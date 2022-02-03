using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNewUnit : MonoBehaviour
{
    public GameObject unit;
    public Vector3 spawnLocation;
    public float trainingTime;

    bool unitPlacementActive = false;
    float currentTrainingTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTrainingTime = trainingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (unitPlacementActive && currentTrainingTime > 0.0f)
        {
            currentTrainingTime -= Time.deltaTime;
            Debug.Log("Training Unit...");
        }

        if (currentTrainingTime <= 0.0f)
        {
            currentTrainingTime = trainingTime;
            PlaceUnitOnClick();
        }
    }

    void PlaceUnitOnClick()
    {
       if (unitPlacementActive)
       {
            Instantiate(unit);
            unit.transform.position = spawnLocation;
       }
                
        unitPlacementActive = false;
    }

    public void ChangePlacementStatus()
    {
        if (unitPlacementActive)
        {
            unitPlacementActive = false;
        }

        if (!unitPlacementActive)
        {
            unitPlacementActive = true;
        }

        Debug.Log(unitPlacementActive);

        //PlaceUnitOnClick();
    }
}
