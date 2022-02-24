using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxAmountCarry;
    public ResourceTypes resourceType;

    public GameObject gold;

    int currentAmountCarry;
    bool inventoryFull;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpResources()
    {
        if (resourceType == ResourceTypes.Gold)
        {

        }

        if (resourceType == ResourceTypes.Food)
        {

        }
    }
}