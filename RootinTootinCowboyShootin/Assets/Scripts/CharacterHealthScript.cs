using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterHealthScript : MonoBehaviour
{
    public float maxHealth = 100.0f; 
    public float currentHealth;

    public HealthBarScript healthbar;
    SpawnManager spawner;

    //turn this script into a base unit stats script to allow for temporary buffs

    //base multipliers for weapon stats
    public float accuracyBuff = 1.0f;
    public float dexterityBuff = 1.0f;
    public float damageBuff = 1.0f;
    public float speedBuff = 1.0f;
    public float defenseBuff = 1.0f;
    //maybe add getters for these above?

    private Vector3 bulletHitLocation;
    private float originalSpeed;
    public Rigidbody rgd;

    public ParticleSystem accuracyBuffParticles;
    public ParticleSystem dexterityBuffParticles;
    public ParticleSystem damageBuffParticles;
    public ParticleSystem speedBuffParticles;
    public ParticleSystem defenseBuffParticles;

    public AudioSource deathSound;

    public List<Buff> activeBuffs;

    public bool dead;
    public float timeBeforeCorpseDecay;
    public float corpseLifetime;

    public float timeBeforeCorpseDelete;


    public void Hit(float damage, Vector3 hitLocation)
    {
        float finalDamageCalc = damage - ((damage * defenseBuff) - damage);

        if (finalDamageCalc < 0f)
            finalDamageCalc = 0;

        currentHealth -= finalDamageCalc;
        bulletHitLocation = hitLocation;
    }

    public float getCurrentHealth()
    {
        if(tag == "Player")
        {
            Debug.Log(GameObject.Find(gameObject.name + "HealthBar").name);
            healthbar = GameObject.Find(gameObject.name + "HealthBar").GetComponent<HealthBarScript>();
            healthbar.SetHealth((int)currentHealth);
        }
        return currentHealth;
    }

    private void RefreshStats() //ensures the particle systems are enabled at the right times and allows use of the public variables
    {
        float accuracyBuffTemp = 0;
        float dexterityBuffTemp = 0;
        float damageBuffTemp = 0;
        float speedBuffTemp = 0;
        float defenseBuffTemp = 0;        

        for (int i = 0; i < activeBuffs.Count; i++)
        {
            // converts the modifier into a percentage that gets added to the finalBuff modifier
            // if the buff is less than 1, it becomes a debuff
            accuracyBuffTemp += (activeBuffs[i].accuracyBuff - 1f);
            dexterityBuffTemp += (activeBuffs[i].dexterityBuff - 1f);
            damageBuffTemp += (activeBuffs[i].damageBuff - 1f);
            speedBuffTemp += (activeBuffs[i].speedBuff - 1f);
            defenseBuffTemp += (activeBuffs[i].defenseBuff - 1f);
        }

        accuracyBuffTemp += 1.0f;
        accuracyBuff = accuracyBuffTemp;
        if(accuracyBuff > 1.0f && accuracyBuffParticles.isPlaying == false)
        {
            accuracyBuffParticles.Play();
        }
        else if(accuracyBuff <= 1.0f && accuracyBuffParticles.isPlaying)
        {
            accuracyBuffParticles.Stop();
        }

        dexterityBuffTemp += 1.0f;
        dexterityBuff = dexterityBuffTemp;
        if (dexterityBuff > 1.0f && dexterityBuffParticles.isPlaying == false)
        {
            dexterityBuffParticles.Play();
        }
        else if (dexterityBuff <= 1.0f && dexterityBuffParticles.isPlaying)
        {
            dexterityBuffParticles.Stop();
        }

        damageBuffTemp += 1.0f;
        damageBuff = damageBuffTemp;
        if (damageBuff > 1.0f && damageBuffParticles.isPlaying == false)
        {
            damageBuffParticles.Play();
        }
        else if (damageBuff <= 1.0f && damageBuffParticles.isPlaying)
        {
            damageBuffParticles.Stop();
        }

        speedBuffTemp += 1.0f;
        speedBuff = speedBuffTemp;
        if (speedBuff > 1.0f && speedBuffParticles.isPlaying == false)
        {
            speedBuffParticles.Play();
        }
        else if (speedBuff <= 1.0f && speedBuffParticles.isPlaying)
        {
            speedBuffParticles.Stop();
        }

        defenseBuffTemp += 1.0f;
        defenseBuff = defenseBuffTemp;
        if (defenseBuff > 1.0f && defenseBuffParticles.isPlaying == false)
        {
            defenseBuffParticles.Play();
        }
        else if (defenseBuff <= 1.0f && defenseBuffParticles.isPlaying)
        {
            defenseBuffParticles.Stop();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = GetComponent<NavMeshAgent>().speed;
        currentHealth = maxHealth;
        if(gameObject.tag == "Player")
        {
            healthbar.SetMaxHealth((int)maxHealth);
        }
        spawner = GameObject.FindObjectOfType<SpawnManager>();
        rgd = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
    }

    void Die()
    {
        if(!dead)
        {
                spawner.enemyDeathsThisWave++;
            rgd.constraints = RigidbodyConstraints.None;
            //rgd.AddExplosionForce(1000f, bulletHitLocation, 5f);
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<WeaponBase>().enabled = false;
            if(this.tag == "Enemy")
            {
                gameObject.GetComponent<EnemyAI>().enabled = false;
            }
            rgd.velocity = (transform.position - bulletHitLocation).normalized * 25f;
            rgd.AddForce(Vector3.up * 2500f);
            rgd.angularVelocity = rgd.velocity;

            if((int)Random.Range(0, 8) == 0)
            {
                deathSound.Play();
            }

            gameObject.tag = "Dead";
            for(int i = 1; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(false);

            dead = true;
        }
        else
        {
            corpseLifetime += Time.deltaTime;
            if(corpseLifetime >= timeBeforeCorpseDecay)
            {
                GetComponent<CapsuleCollider>().enabled = false;
                //StartCoroutine(new WaitForSecondsRealtime(2.0f));
                //Destroy(gameObject);
            }
            if(corpseLifetime >= timeBeforeCorpseDelete)
            {
                Destroy(gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        RefreshStats();
    }

    void CheckHealth() 
    {
        if(currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;
            Die();
            if(gameObject.CompareTag("Enemy"))
            {
                spawner.enemyDeathsThisWave++;
            }
            //Destroy(gameObject);
        }
        else
        {
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
}