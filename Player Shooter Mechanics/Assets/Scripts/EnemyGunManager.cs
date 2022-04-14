using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunManager : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawnPoint;

    public float bulletSpeed;
    public float range;
    public float shootingCooldown;
    public int maxBulletCount;
    public int damage;

    int currentBulletCount; //idk if the enemy needs to reload or anything tbh
    float currentShootingCooldown;

    // Start is called before the first frame update
    void Start()
    {
        currentShootingCooldown = shootingCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        currentShootingCooldown -= Time.deltaTime;

        if (currentShootingCooldown <= 0.0f)
        {
            currentShootingCooldown = shootingCooldown;
            FireBullet();
        }
    }

    public void FireBullet()
    {
        GameObject tmp;

        currentBulletCount--;
        tmp = Instantiate(bullet, bulletSpawnPoint);

        tmp.transform.position = bulletSpawnPoint.position;
        tmp.GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, bulletSpeed);
    }
}
