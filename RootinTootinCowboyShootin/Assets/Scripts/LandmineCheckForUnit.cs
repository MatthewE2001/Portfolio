using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LandmineCheckForUnit : MonoBehaviour
{
    public float detectionRadius;
    public float explosionRadius;
    public float damage;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //explode instantly
        if(detectionRadius == -1)
        {
            Explode();
        }
        Collider[] detectionRangeColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in detectionRangeColliders)
        {
            if (collider.tag == "Enemy")
            {
                Explode();
            }
        }

    }

    void Explode()
    {
        Collider[] damageRangeColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in damageRangeColliders)
        {
            if (collider.tag == "Enemy" || collider.tag == "Player")
            {
                collider.GetComponent<CharacterHealthScript>().currentHealth -= damage;
            }
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
