using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassTracker : MonoBehaviour
{
    public string Class; //uppercase for a reason
    private PartyTracker party;
    public void AddToPartyTracker()
    {
        switch(Class)
        {
            case "Lawman":
                {
                    PartyTracker.instance.AddLawman();
                    transform.parent = PartyTracker.instance.transform;
                    break;
                }
            case "Cowboy":
                {
                    transform.parent = PartyTracker.instance.transform;
                    PartyTracker.instance.AddCowboy();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void RemoveFromPartyTracker()
    {
        switch (Class)
        {
            case "Lawman":
                {
                    PartyTracker.instance.lawmen--;
                    break;
                }
            case "Cowboy":
                {
                    PartyTracker.instance.cowboys--;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //AddToPartyTracker();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
