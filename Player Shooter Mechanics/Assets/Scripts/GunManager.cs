using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bullet;

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

        if (Input.GetMouseButtonDown(0) && currentBulletCount > 0)
        {
            FireGun();
        }
    }

    int GetDamageDealt()
    {
        return damage;
    }

    public void FireGun()
    {
        GameObject tmp;

        currentBulletCount--;
        tmp = Instantiate(bullet, bulletSpawnPoint);

        tmp.transform.position = bulletSpawnPoint.position;
        tmp.GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, bulletSpeed);
    }
}