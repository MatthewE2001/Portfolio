using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ability : MonoBehaviour
{
    public float cooldownInSeconds;
    public bool canUse;
    //public bool noCooldown;
    public float cdProgress;

    private IEnumerator cooldownRoutine;
    public string abilityName;

    [System.Serializable]
    public class AbilityEvent : UnityEvent { }

    public AbilityEvent abilityEvent = new AbilityEvent();

    public AbilityEvent onAbilityEvent { get { return abilityEvent; } set { abilityEvent = value; } }


    public void AbilityEventTriggered()
    {
        if(canUse)
        {
            ((MonoBehaviour)abilityEvent.GetPersistentTarget(0)).SendMessage(abilityEvent.GetPersistentMethodName(0));
            //canUse = false;
            cdProgress = cooldownInSeconds;
        }
        else
        {
            Debug.Log("On Cooldown");
            //insert a sound here or something
        }
    }

    //IEnumerator StartCooldown()
    //{
    //    while(true)
    //    {
    //        if(!canUse)
    //        {
    //            yield return new WaitForSeconds(cooldownInSeconds);
    //            canUse = true;
    //        }
    //        yield return new WaitForSeconds(0);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        //cooldownRoutine = StartCooldown();
        //StartCoroutine(cooldownRoutine);
    }

    // Update is called once per frame
    void Update()
    {
        if(canUse == false)
        {
            cdProgress -= Time.deltaTime;
            if(cdProgress <= 0)
            {
                canUse = true;
                cdProgress = cooldownInSeconds;
            }
        }
    }
}
