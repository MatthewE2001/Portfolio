using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script only exists so the dead units that fall under the map get destroyed
//also acts as a failsafe in case some enemy unit somehow falls under the map

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Enemy")
        {
            Debug.Log("Destroyed: " + other.name);
            Destroy(other); 
        }
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
