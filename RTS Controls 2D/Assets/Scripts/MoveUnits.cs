using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnits : MonoBehaviour
{
    public Camera mainCamera;

    Vector3 worldMousePosition;
    Vector3 endPosition;
    Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        
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
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            colliders = Physics2D.OverlapAreaAll(worldMousePosition, 
                mainCamera.ScreenToWorldPoint(Input.mousePosition));

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.GetComponent<Movement>().ChangeMoveActive();
                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            endPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            List<Vector3> diffMovePositions = new List<Vector3>
            { 
                endPosition,
                endPosition + new Vector3 (2, 0),
                endPosition + new Vector3 (0, 2),
                endPosition + new Vector3 (-2, 0),
                endPosition + new Vector3 (0, -2),
                endPosition + new Vector3 (2, 2),
                endPosition + new Vector3 (2, -2),
                endPosition + new Vector3 (-2, -2),
                endPosition + new Vector3 (-2, 2)
            };

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
