using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAdjustment : MonoBehaviour
{
    public Slider timeSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTimeScale()
    {        
        Time.timeScale = timeSlider.value;
        Debug.Log(Time.timeScale);
    }
}