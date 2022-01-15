using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{
    //public GameObject hudPrefab;
    public List<GameObject> hudObjects;
    public List<GameObject> playerHudLink;
    public int hudsActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateNewUI(GameObject unit)
    {
        hudObjects[hudsActive].SetActive(true);
        //hudObjects[hudsActive]
    }
}
