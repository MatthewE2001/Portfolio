using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineAbility : MonoBehaviour
{
    public float range;
    public GameObject abilityPrefab;
    public bool usingAbility;
    private Ability abilityController;
    private AbilityUI abilityUI;
    public GameObject previewPrefab;
    private GameObject transparentModel;
    public void UseAbility()
    {
        usingAbility = true;
        //base.UseAbility();
        Debug.Log("Landmine use");
        abilityUI.abilityInfo.text = "Place Landmine \n (Left Mouse) \n (Right Mouse to Cancel)";
        if(transparentModel == null)
        {
            transparentModel = Instantiate(previewPrefab);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //canUse = true;
        abilityController = GetComponent<Ability>();
        abilityUI = FindObjectOfType<AbilityUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(usingAbility)
        {
            if(Input.GetMouseButtonDown(0))
            {
                AttemptPlaceMine();
            }
            if (Input.GetMouseButtonDown(1))
            {
                usingAbility = false;
            }
            ShowPreview();
        }
        else
        {
            if (transparentModel != null)
            {
                Destroy(transparentModel);
            }
        }
    }

    void AttemptPlaceMine()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 mousePos = new Vector3(transform.position.x, transform.position.y, transform.position.z + range + 1);

        if (Physics.Raycast(ray, out hit))
        {
            mousePos = hit.point;
        }
        float dist = Vector3.Distance(mousePos, transform.position);
        if (dist < range)
        {
            Debug.Log("Placed Mine");
            Instantiate(abilityPrefab, mousePos, Quaternion.identity);
            usingAbility = false;
            abilityController.canUse = false; //triggers cooldown only when placed
            Destroy(transparentModel);
        }
    }
    
    void ShowPreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 mousePos = new Vector3(transform.position.x, transform.position.y, transform.position.z + range + 1);

        int layer = 1 << 8;
        Debug.Log(layer);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            mousePos = hit.point;
            float dist = Vector3.Distance(mousePos, transform.position);
            if (dist < range)
            {
                transparentModel.transform.position = mousePos;
            }
        }
        
    }
}
