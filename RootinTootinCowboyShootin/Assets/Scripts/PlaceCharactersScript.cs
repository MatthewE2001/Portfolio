using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class PlaceCharactersScript : MonoBehaviour
{
    Vector3 mousePos;
    public GameObject thePrefab;

    // Start is called before the first frame update
    void Start()
    {
        mousePos = new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.z);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        UnityEngine.Debug.DrawRay(ray.origin, ray.direction, Color.green);

        CheckInput();
    }

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnUnit();
        }
    }

    void SpawnUnit()
    {
        Vector3 wordPos;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        UnityEngine.Debug.DrawRay(ray.origin, ray.direction, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            wordPos = hit.point;
        }
        else
        {
            wordPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
        Instantiate(thePrefab, wordPos, Quaternion.identity);
    }
}
