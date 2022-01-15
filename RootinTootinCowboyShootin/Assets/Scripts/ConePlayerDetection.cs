using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConePlayerDetection : MonoBehaviour
{
    public float range;
    public float FoVDetectionAngle;
    public float damage;
    Vector3 hitDirection;
    bool usingAbility = false;
    public Ability abilityController;
    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && usingAbility)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 dir = hit.point - transform.position;
                transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0));
                FireCone();
            }
            usingAbility = false;
        }
        if(Input.GetMouseButtonDown(1))
        {
            usingAbility = false;
        }
    }

    public void UseAbility()
    {
        usingAbility = true;
    }

    public void FireCone()
    {
        string tag = "Player";
        if(this.tag == "Player")
        {
            tag = "Enemy";
        }
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in targets)
        {
            if (Mathf.Abs(Vector3.Angle(transform.forward, (target.transform.position - transform.position).normalized)) < FoVDetectionAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit, range))
                {
                    Debug.DrawRay(transform.position, (target.transform.position - transform.position).normalized * range, Color.red);
                    Debug.Log("raycast hit something:");
                    if (hit.collider.gameObject.tag == tag)
                    {
                        Debug.Log("raycast hit unit");
                        target.GetComponent<CharacterHealthScript>().currentHealth -= damage;
                        
                    }
                }
            }
        }
        abilityController.canUse = false;
    }
}