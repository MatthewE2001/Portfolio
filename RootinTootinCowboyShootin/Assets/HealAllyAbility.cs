using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealAllyAbility : MonoBehaviour
{
    public float range;
    public float healAmount;
    public bool usingAbility;
    public bool abilityTarChosen;
    private Ability abilityController;
    private AbilityUI abilityUI;
    Vector3 abilityPlacementLocations;
    bool placeChosen;
    private GameObject healTar = null;
    public void UseAbility()
    {
        usingAbility = true;
        //base.UseAbility();
        Debug.Log("Heal used");
        abilityUI.abilityInfo.text = "Heal Friendly \n (Left Mouse) \n (Right Mouse to Cancel)";
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
        if (usingAbility)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttemptHealTarget();
            }
            if (Input.GetMouseButtonDown(1))
            {
                usingAbility = false;
            }
        }
        if(abilityTarChosen)
        {
            AttemptHeal();
        }
    }

    void AttemptHealTarget()
    {
        Debug.Log("Heal attempted");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 mousePos = new Vector3(transform.position.x, transform.position.y, transform.position.z + range + 1);

 

        if (Physics.Raycast(ray, out hit))
        {
            mousePos = hit.point;
            if (hit.collider.tag == this.gameObject.tag)
            {
                healTar = hit.collider.gameObject;
                abilityPlacementLocations = healTar.transform.position;
                abilityTarChosen = true;
                usingAbility = false;
                MoveTowardUseSpot();
            }
        }

    }

    void AttemptHeal()
    {
        float dist = Vector3.Distance(healTar.transform.position, transform.position);
        if (dist < range && healTar != null)
        {
            Debug.Log("healed ally");
            //heal
            healTar.GetComponent<CharacterHealthScript>().currentHealth += healAmount;

            if (healTar.GetComponent<CharacterHealthScript>().currentHealth > healTar.GetComponent<CharacterHealthScript>().maxHealth)
            {
                healTar.GetComponent<CharacterHealthScript>().currentHealth = healTar.GetComponent<CharacterHealthScript>().maxHealth;
            }
            
            abilityTarChosen = false;
            abilityController.canUse = false; //triggers cooldown only when used
            this.GetComponent<NavMeshAgent>().SetDestination(this.transform.position);
        }
    }

    void MoveTowardUseSpot()
    {
        Debug.Log("Move toward set");
        this.GetComponent<NavMeshAgent>().SetDestination(abilityPlacementLocations);
    }
}
