using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAura : MonoBehaviour
{

    public Material accuracyMat;
    public Material dexterityMat;
    public Material damageMat;
    public Material speedMat;
    public Material defenseMat;
    private MeshRenderer auraCircle;
    public float buffRadius;

    public Buff buffToGive;
    public LayerMask unitLayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            other.gameObject.GetComponent<CharacterHealthScript>().activeBuffs.Add(buffToGive);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == unitLayer)
        {
            other.gameObject.GetComponent<CharacterHealthScript>().activeBuffs.Add(buffToGive);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            other.gameObject.GetComponent<CharacterHealthScript>().activeBuffs.Remove(buffToGive);
        }
    }

    void PickMaterial()
    {
        if (buffToGive.accuracyBuff != 1.0f)
        {
            auraCircle.material = accuracyMat;
        }
        if (buffToGive.dexterityBuff != 1.0f)
        {
            auraCircle.material = dexterityMat;
        }
        if (buffToGive.damageBuff != 1.0f)
        {
            auraCircle.material = damageMat;
        }
        if (buffToGive.speedBuff != 1.0f)
        {
            auraCircle.material = speedMat;
        }
        if (buffToGive.defenseBuff != 1.0f)
        {
            auraCircle.material = defenseMat;
        }
    }

    void CheckRadius()
    {
        //transform.localScale = new Vector3(buffRadius, 1.0f, buffRadius);
        //Collider[] colliders = Physics.OverlapSphere(transform.position, buffRadius, unitLayer);
        //Debug.Log(colliders.Length);
    }

    // Start is called before the first frame update
    void Start()
    {
        auraCircle = GetComponent<MeshRenderer>();
        //buffToGive = new Buff(accuracyBuff, dexterityBuff, damageBuff, speedBuff, defenseBuff);
        PickMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRadius();
    }
}
