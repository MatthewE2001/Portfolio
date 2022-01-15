using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public AttackUnitInRange unitRange;
    private MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(unitRange.range, unitRange.range, unitRange.range);
        
        if (GameObject.FindObjectOfType<UnitSelector>().selectedUnit == transform.parent.gameObject)
            renderer.enabled = true;
        else
            renderer.enabled = false;
    }
}
