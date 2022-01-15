using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Ability selectedPlayerAbility;
    private GameObject selectedUnitObject;
    public TextMeshProUGUI abilityInfo;
    public GameObject abilityUIPanel;
    //Ability 


    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            selectedPlayerAbility.AbilityEventTriggered();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //SelectPlayer();
        }
    }

    public void SetText()
    {
        if(selectedPlayerAbility.canUse && selectedPlayerAbility != null)
        {
            abilityInfo.gameObject.SetActive(true);
            abilityInfo.text = selectedPlayerAbility.abilityName + "\nReady";
        }
        else
        {
            abilityInfo.gameObject.SetActive(true);
            abilityInfo.text = selectedPlayerAbility.abilityName + "\n" + selectedPlayerAbility.cdProgress.ToString("F2");
        }
    }


    void SelectPlayerRaycast() //this functionality has been removed, keeping it here just in case
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                selectedPlayerAbility = hit.collider.gameObject.GetComponent<Ability>();
                selectedUnitObject = hit.collider.gameObject;
                gameObject.GetComponent<Image>().enabled = true;
                abilityInfo.enabled = true;
            }
            else
            {
                //gameObject.GetComponent<Image>().enabled = false;
                //abilityInfo.enabled = false;
            }
        }
    }

    public void SelectPlayer(GameObject selectedUnit)
    {
        selectedUnitObject = selectedUnit;
        selectedPlayerAbility = selectedUnitObject.GetComponent<Ability>();
        gameObject.GetComponent<Image>().enabled = true;
        abilityInfo.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedPlayerAbility = GameObject.FindObjectOfType<Ability>();
        //gameObject.GetComponent<Image>().enabled = false;
        //abilityInfo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        SetText();
    }
}
