using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains functionality of passive buffs and time-based buffs depending on permanent bool
public class Buff : MonoBehaviour
{
    public float accuracyBuff = 1.0f;
   // public bool enableAccuracyParticles;
    
    public float dexterityBuff = 1.0f;
    //public bool enableDexterityParticles;

    public float damageBuff = 1.0f;
    //public bool enableDamageParticles;

    public float speedBuff = 1.0f;
    //public bool enableSpeedParticles;

    public float defenseBuff = 1.0f;
    //public bool enableDefenseParticles;

    public bool permanent; //turns this script into a passive ability if true
    public float duration;
    public float buffTimeRemaining;
    private CharacterHealthScript stats;
    public bool isAura;
    


    public Buff(float accuracy, float dexterity, float damage, float speed, float defense, bool aura = false, bool isPermanent = true)
    {
        accuracyBuff = accuracy;
        dexterityBuff = dexterity;
        damageBuff = damage;
        speedBuff = speed;
        defenseBuff = defense;
        isAura = aura;
        permanent = isPermanent;
    }

    public void RemoveBuff()
    {
        //Destroy(this);
    }

    public void UseAbility()
    {
        if(!permanent)
        {
            GetComponent<Ability>().canUse = false;
            buffTimeRemaining = duration;
            stats.activeBuffs.Add(this);
            
        }
    }

    //public void ActivateParticles()
    //{
    //    if(enableAccuracyParticles)
    //    {
    //        stats.accuracyBuffParticles.enableEmission = true;
    //    }
    //    if (enableDexterityParticles)
    //    {
    //        stats.dexterityBuffParticles.enableEmission = true;
    //    }
    //    if (enableDamageParticles)
    //    {
    //        stats.damageBuffParticles.enableEmission = true;
    //    }
    //    if (enableSpeedParticles)
    //    {
    //        stats.speedBuffParticles.enableEmission = true;
    //    }
    //    if (enableDefenseParticles)
    //    {
    //        stats.defenseBuffParticles.enableEmission = true;
    //    }
    //}

    //public void DeactivateParticles()
    //{
    //    if (enableAccuracyParticles)
    //    {
    //        stats.accuracyBuffParticles.enableEmission = false;
    //    }
    //    if (enableDexterityParticles)
    //    {
    //        stats.dexterityBuffParticles.enableEmission = false;
    //    }
    //    if (enableDamageParticles)
    //    {
    //        stats.damageBuffParticles.enableEmission = false;
    //    }
    //    if (enableSpeedParticles)
    //    {
    //        stats.speedBuffParticles.enableEmission = false;
    //    }
    //    if (enableDefenseParticles)
    //    {
    //        stats.defenseBuffParticles.enableEmission = false;
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        if(!isAura)
        {
            stats = GetComponent<CharacterHealthScript>();
        
            if(permanent)
            {
                //apply buff
                stats.activeBuffs.Add(this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!permanent && !isAura)
        {
            if(buffTimeRemaining > 0)
            {
                buffTimeRemaining -= Time.deltaTime;
            }
            else if(stats.activeBuffs.Contains(this))
            {
                stats.activeBuffs.Remove(this);
            }
        }
    }
}
