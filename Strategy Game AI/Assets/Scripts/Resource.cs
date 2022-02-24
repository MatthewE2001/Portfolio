using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceTypes
{
    Gold,
    Food
}

public class Resource : MonoBehaviour
{
    public int startAmountOfResource;

    bool resourceRemaining;
    int currentResourceAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (resourceRemaining == false)
        {
            currentResourceAmount = startAmountOfResource; //just to reset the resource points for simplicity
        }
    }
}