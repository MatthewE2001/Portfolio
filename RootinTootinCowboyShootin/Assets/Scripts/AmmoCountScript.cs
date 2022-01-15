using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCountScript : MonoBehaviour
{
    public Image ammoImage;
    float ammoCount;
    public float bulletNumber;

    public WeaponBase weapon;
    public string unitName;
    Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find(unitName).GetComponent<WeaponBase>();
        ammoCount = weapon.GetComponent<WeaponBase>().fullMagazine;
        ammoImage = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if(weapon != null)
        {

            ammoCount = weapon.GetComponent<WeaponBase>().ammoRemaining;

            if (ammoCount >= bulletNumber)
            {
                ammoImage.enabled = true;
            }
            else
            {
                ammoImage.enabled = false;
            }
        }
        else
        {
            weapon = GameObject.Find(unitName).GetComponent<WeaponBase>();
        }
    }

}
