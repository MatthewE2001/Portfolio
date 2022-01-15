using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDamageBuff : MonoBehaviour
{

    float damageBuffModifier;
    float duration;

    CharacterHealthScript stats;

    public void UseAbility()
    {
        stats.damageBuff = damageBuffModifier;
        //Buff buff = new Buff()
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<CharacterHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
