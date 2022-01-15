using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Visual representation of health using 3DText, plan to replace with health bars/add some other kind of feedback
public class TextHealthScript : MonoBehaviour
{
    private TextMesh text;
    public CharacterHealthScript health;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);

        text.text = health.getCurrentHealth().ToString();
    }
}