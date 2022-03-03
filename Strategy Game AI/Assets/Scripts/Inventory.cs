using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxAmountCarry;
    public float collectionTime;
    public ResourceTypes resourceType;

    int currentAmountCarry;
    float currentCollectionTime;
    bool inventoryFull; //this could be totally unneeded
    bool collectionInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        currentCollectionTime = collectionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (collectionInProgress == true)
        {            
            if (gameObject.GetComponent<MoveAI>().movePosition == GameObject.Find("Food").transform.position)
            {
                resourceType = ResourceTypes.Food;
            }

            if (gameObject.GetComponent<MoveAI>().movePosition == GameObject.Find("Gold").transform.position)
            {
                resourceType = ResourceTypes.Gold;
            }

            //currentCollectionTime = collectionTime;
            PickUpResources();
            collectionInProgress = false;            
        }
    }

    public void BeginGathering()
    {
        collectionInProgress = true;
    }

    public void PickUpResources()
    {
        if (resourceType == ResourceTypes.Gold)
        {
            currentAmountCarry = maxAmountCarry;
            inventoryFull = true;
        }

        if (resourceType == ResourceTypes.Food)
        {
            currentAmountCarry = maxAmountCarry;
            inventoryFull = true;
        }
    }

    public void EndPickup()
    {
        //currentCollectionTime = collectionTime;
        PickUpResources();
        collectionInProgress = false;
    }

    public void ChangeResourceType(ResourceTypes type)
    {
        resourceType = type;
    }

    public void ChangeCollectionStatus(bool val)
    {
        collectionInProgress = val;
    }
    
    public void ChangeInventoryStatus() //maybe give this parameters idk
    {
        currentAmountCarry = 0;
        inventoryFull = false;
    }

    public void ResetCollectionTime()
    {
        currentCollectionTime = collectionTime;
    }

    public float GetCurrentCollectionTime()
    {
        return currentCollectionTime;
    }

    public int GetCurrentAmountCarried()
    {
        return currentAmountCarry;
    }

    public bool GetCollectionStatus()
    {
        return collectionInProgress;
    }

    public bool GetInventoryStatus()
    {
        return inventoryFull;
    }
}