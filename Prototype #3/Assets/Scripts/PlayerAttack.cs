using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject player; //in case I need to reference the player
    bool chargeCoolDown = false;
    float coolDownTime = 10.0f;
    public float attackStrength = 500;
    bool weaponMoved = false;
    // Start is called before the first frame update
    void Start()
    {
        attackStrength = GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetDamage();
    }

    // Update is called once per frame
    void Update()
    {
        ChargeAttack();

        if (chargeCoolDown == true && GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetActiveGolem() == 2)
        {
            coolDownTime -= Time.deltaTime;

            Debug.Log(coolDownTime);

            if (coolDownTime <= 20.0f) //so the ability runs for ten seconds
            {
                GameObject.Find("GameManager").GetComponent<ChangeGolems>().ChangeAttackGolemStats(3.0f, 6);
            }

            if (coolDownTime <= 0.0f) //then the cool down and not usable period is another twenty seconds
            {
                coolDownTime = 20.0f;
                chargeCoolDown = false;
            }
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            this.GetComponent<BoxCollider>().isTrigger = true;

            if (weaponMoved == false)
            {
                //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.x + 0.25f);
                weaponMoved = true;
            }
        }    
        else if (Input.GetKey(KeyCode.Mouse2) != true)
        {
            this.GetComponent<BoxCollider>().isTrigger = false;

            if (weaponMoved == true)
            {
                //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.x - 0.25f);
                weaponMoved = false;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyAttack>().LowerEnemyHealth(attackStrength);
            Debug.Log("This is getting here");
        }
    }

    void ChargeAttack()
    {
        //this is the charge attack that should only be able to run when 
        if (GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetActiveGolem() == 2)
        {
            if (Input.GetKey(KeyCode.E) && chargeCoolDown == false) //E is just a random key I picked
            {
                //set the player attack speed up a bit and also their strength too (probably strength too)
                GameObject.Find("GameManager").GetComponent<ChangeGolems>().ChangeAttackGolemStats(6.0f, 12);
                chargeCoolDown = true;
            }
        }
    }
}
