using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnits : MonoBehaviour
{
    public Camera mainCamera;

    Vector3 worldMousePosition;
    Vector3 endPosition;
    Collider2D[] colliders;
    string formation;
    List<Vector3> diffMovePositions;
    Vector3[] tmpPositions;

    // Start is called before the first frame update
    void Start()
    {
        tmpPositions = GameObject.Find("GameManager").GetComponent<UnitFormations>().GetSquarePositions();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUnitSelection();
    }

    public void CheckUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            worldMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            colliders = Physics2D.OverlapAreaAll(worldMousePosition, 
                mainCamera.ScreenToWorldPoint(Input.mousePosition));

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.GetComponent<Movement>().ChangeMoveActive();
                colliders[i].gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            endPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            formation = GameObject.Find("GameManager").GetComponent<UnitFormations>().GetCurrentFormation();

            if (formation == "Square")
            {
                tmpPositions = GameObject.Find("GameManager").GetComponent<UnitFormations>().GetSquarePositions();

                diffMovePositions = new List<Vector3>
                {
                    endPosition,
                    endPosition + tmpPositions[1],
                    endPosition + tmpPositions[2],
                    endPosition + tmpPositions[3],
                    endPosition + tmpPositions[4],
                    endPosition + tmpPositions[5]
                };
            }

            if (formation == "Circle")
            {
                tmpPositions = GameObject.Find("GameManager").GetComponent<UnitFormations>().GetCirclePositions();

                diffMovePositions = new List<Vector3>
                {
                    endPosition,
                    endPosition + tmpPositions[1],
                    endPosition + tmpPositions[2],
                    endPosition + tmpPositions[3],
                    endPosition + tmpPositions[4],
                    endPosition + tmpPositions[5],
                    endPosition + tmpPositions[6],
                    endPosition + tmpPositions[7],
                    endPosition + tmpPositions[8]
                };
            }

            if (formation == "Line")
            {
                tmpPositions = GameObject.Find("GameManager").GetComponent<UnitFormations>().GetLinePositions();

                diffMovePositions = new List<Vector3>
                {
                    endPosition,
                    endPosition + tmpPositions[1],
                    endPosition + tmpPositions[2],
                    endPosition + tmpPositions[3],
                    endPosition + tmpPositions[4],
                    endPosition + tmpPositions[5]
                };
            }

            if (formation == "Diamond")
            {
                tmpPositions = GameObject.Find("GameManager").GetComponent<UnitFormations>().GetDiamondPositions();

                diffMovePositions = new List<Vector3>
                {
                    endPosition,
                    endPosition + tmpPositions[1],
                    endPosition + tmpPositions[2],
                    endPosition + tmpPositions[3],
                    endPosition + tmpPositions[4],
                    endPosition + tmpPositions[5]
                };
            }

            int tmpListCount = 0;

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.GetComponent<Movement>().SetMoveLocation
                    (diffMovePositions[tmpListCount]);
                tmpListCount++;
                tmpListCount = tmpListCount % diffMovePositions.Count;
            }
        }
    }
}
