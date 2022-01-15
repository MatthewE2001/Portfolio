using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRangeIndicator : MonoBehaviour
{
    [SerializeField]
    float newScale;
    public WeaponBase weapon;
    // Start is called before the first frame update
    void Start()
    {
        newScale = weapon.newRange + 1f;
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
