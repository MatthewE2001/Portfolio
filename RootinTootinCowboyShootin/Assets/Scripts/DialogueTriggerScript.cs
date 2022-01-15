using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerScript : MonoBehaviour
{
    public bool canInteract = false;
    public string NPCName;
    public string PlayerName;
    public Dialogue dialogue;
    public TextMesh prompt;
    void Start()
    {
        dialogue = GetComponent<Dialogue>();
    }

    void Update()
    {
        ActivateDialogue();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            canInteract = true;
            UnityEngine.Debug.Log("Entered Trigger");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canInteract = false;
            UnityEngine.Debug.Log("Exited Trigger");
        }
    }

    void ActivateDialogue()
    {
        prompt.gameObject.SetActive(canInteract);
        if (canInteract == true && Input.GetKeyDown(KeyCode.G))
        {
            canInteract = false;
            StartCoroutine(dialogue.Type());
            //dialogue.NextSentence();
        }
    }
}
