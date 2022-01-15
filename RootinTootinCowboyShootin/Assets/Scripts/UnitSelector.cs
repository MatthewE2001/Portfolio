using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public GameObject selectedUnit;

    public void SelectUnit(GameObject unitToSelect)
    {
        selectedUnit = unitToSelect;
        GameObject.FindObjectOfType<AbilityUI>().SelectPlayer(selectedUnit);
    }

    public GameObject GetSelectedUnit()
    {
        return selectedUnit;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
