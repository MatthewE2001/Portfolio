using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownScript : MonoBehaviour
{
    public Image imageTimer;
    float cooldown;
    public Ability ability;
    bool isCooldown;
    public string unitName;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = ability.GetComponent<Ability>().cooldownInSeconds;
        isCooldown = ability.GetComponent<Ability>().canUse;
        Debug.Log(GameObject.Find(unitName).name);
        ability = GameObject.Find(unitName).GetComponent<Ability>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ability != null)
        {

        isCooldown = ability.GetComponent<Ability>().canUse;

        if (isCooldown)
        {
            imageTimer.fillAmount = 0;
        }
        else
        {
            imageTimer.fillAmount += 1 / cooldown * Time.deltaTime;
        }
        }
    }
}
