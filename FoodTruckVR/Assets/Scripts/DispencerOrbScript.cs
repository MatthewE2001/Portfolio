using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispencerOrbScript : MonoBehaviour
{
    public IngredientsManager.GrabbableItemName itemName;

    public float delayBeforeSpawningIngridients;

    // Item inside orb
    GameObject itemInside = null;
    GrabbableItemScript itemInsideGrabbableScript;

    // Start is called before the first frame update
    void Start()
    {
        if (itemName != IngredientsManager.GrabbableItemName.NumOfIngridients)
        {
            itemInside = IngredientsManager.Instance.SpawnItem(itemName);
            itemInside.transform.position = transform.position;

            if(itemInside.GetComponent<GrabbableItemScript>() != null)
                itemInsideGrabbableScript = itemInside.GetComponent<GrabbableItemScript>();              
            else
                Debug.LogError("Dispensing sphere cannot find grabbable script");
            
            itemInsideGrabbableScript.OnEnterOrbWithoutParenting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When food item is taken
        if (other.gameObject == itemInside)
        {
            itemInsideGrabbableScript.OnExitOrbWithoutParenting();

            StopAllCoroutines();
            StartCoroutine(WaitForTimeAndSpawn(delayBeforeSpawningIngridients));
        }

    }

    IEnumerator WaitForTimeAndSpawn(float time)
    {
        yield return new WaitForSeconds(time);

        itemInside = IngredientsManager.Instance.SpawnItem(itemName);
        itemInside.transform.position = transform.position;
        itemInsideGrabbableScript = itemInside.GetComponent<GrabbableItemScript>();

        itemInsideGrabbableScript.OnEnterOrbWithoutParenting();
    }
}
