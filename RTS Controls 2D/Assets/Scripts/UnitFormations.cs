using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFormations : MonoBehaviour
{
    public Vector3[] diamondPositions;
    public Vector3[] linePositions;
    public Vector3[] squarePositions;
    public Vector3[] circlePositions;

    string currentFormation;

    // Start is called before the first frame update
    void Start()
    {
        currentFormation = "Square"; //just to give it a starting value
    }

    // Update is called once per frame
    void Update()
    {
        ChangeUnitFormation();
    }

    void ChangeUnitFormation()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //change the unit formation to circle
            currentFormation = "Circle";
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //change unit formation to diamond?
            currentFormation = "Diamond";
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //change unit formation to a line
            currentFormation = "Line";
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            currentFormation = "Square";
        }
    }

    public string GetCurrentFormation()
    {
        return currentFormation;
    }

    public Vector3[] GetSquarePositions()
    {
        return squarePositions;
    }

    public Vector3[] GetDiamondPositions()
    {
        return diamondPositions;
    }

    public Vector3[] GetLinePositions()
    {
        return linePositions;
    }

    public Vector3[] GetCirclePositions()
    {
        return circlePositions;
    }
}
