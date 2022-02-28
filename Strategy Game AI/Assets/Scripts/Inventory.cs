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
    bool collectionInProgress;

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
            currentCollectionTime -= Time.deltaTime;

            if (currentCollectionTime <= 0.0f)
            {
                currentCollectionTime = collectionTime;
                PickUpResources();
                collectionInProgress = false;
            }
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

    public void ChangeResourceType(ResourceTypes type)
    {
        resourceType = type;
    }

    public void ChangeCollectionStatus(bool val)
    {
        collectionInProgress = val;
    }

    public float GetCurrentCollectionTime()
    {
        return currentCollectionTime;
    }
}