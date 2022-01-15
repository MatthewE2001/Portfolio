using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemSelectionUI : MonoBehaviour
{
    public Text exploreGolemInfo, attackGolemInfo, mapGolemInfo, statementText;
    public Button exploreGolemButton, attackGolemButton, mapGolemButton;
    //public Image exploreGolemImage, attackGolemImage, mapGolemImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExploreSelected()
    {
        //change golem active to 1 and then put the player into the scene at their current respawn point
    }

    public void AttackSelected()
    {
        //change golem active to 2 and then put the player into the scene at their current respawn point
    }

    public void MapSelected()
    {
        //change golem active to 3 and then put the player into the scene at their current respawn point
    }
}
