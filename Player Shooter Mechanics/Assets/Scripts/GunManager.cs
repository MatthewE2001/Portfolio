using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Transform bulletSpawnPoint;

    public float bulletSpeed;
    public float range;
    public int maxBulletCount;
    public int damage;

    int currentBulletCount;

    // Start is called before the first frame update
    void Start()
    {
        currentBulletCount = maxBulletCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) //reload
        {
            currentBulletCount = maxBulletCount;
        }
    }

    int GetDamageDealt()
    {
        return damage;
    }
}