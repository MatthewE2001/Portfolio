using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public CustomerSystem customerSystem;
    //[HideInInspector]
    public int currentApproval = 0;
    public GameObject insidePartOfMeter;

    public int maxApproval;

    GameObject wristHUD;
    WristHUD wristHUDScript;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        wristHUD = GameObject.Find("WristHUD");
        wristHUDScript = wristHUD.GetComponent<WristHUD>();
        wristHUDScript.Initialize();
        wristHUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        wristHUDScript.UpdateHUDPosition();
    }

    public void AddApproval(int addedValue)
    {
        currentApproval += addedValue;

        if (currentApproval < 0)
            currentApproval = 0;

        if (currentApproval > maxApproval)
            currentApproval = maxApproval;

        updateApprovalMeter();
    }

    public void updateApprovalMeter()
    {
        Vector3 newLocalScale = insidePartOfMeter.transform.localScale;
        newLocalScale.y = 0.9f / maxApproval * currentApproval;
        insidePartOfMeter.transform.localScale = newLocalScale;

        Vector3 newLocalPosition = insidePartOfMeter.transform.localPosition;
        newLocalPosition.y = -0.45f + newLocalScale.y / 2f;
        insidePartOfMeter.transform.localPosition = newLocalPosition;
    }

    public void SpawnCustomer()
    {
        if (customerSystem != null)
            customerSystem.SpawnNewCustomer();
    }

    public void StartNextDay()
    {
        if (currentApproval != maxApproval)
        {
            currentApproval -= (currentApproval / 5);
        }

        customerSystem.NewDayStart();
    }

    public GameObject GetWristHUDGameObject()
    {
        return wristHUD;
    }
}
