using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [System.Serializable]
    public class WeaponEvent : UnityEvent { }

    public WeaponEvent weaponEvent = new WeaponEvent();

    public WeaponEvent onWeaponEvent { get { return weaponEvent; } set { weaponEvent = value; } }



    public void WeaponEventTriggered()
    {
        ((MonoBehaviour)weaponEvent.GetPersistentTarget(0)).SendMessage(weaponEvent.GetPersistentMethodName(0));
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
