using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public bool recruitable;
    public GameObject prefabUnitToRecruit;
    public GameObject inactiveUnitToRecruit;


    void Start()
    {
        
    }

    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    public IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            if(recruitable)
            {
                //Recruit();
                RecruitInactiveObject();
            }
        }
    }

    public void Recruit()
    {
        GameObject recruit = Instantiate(prefabUnitToRecruit, transform);
        recruit.transform.parent = null;
        Destroy(gameObject);
    }

    public void RecruitInactiveObject()
    {
        //GameObject recruit = Instantiate(prefabUnitToRecruit, transform);
        inactiveUnitToRecruit.SetActive(true);
        inactiveUnitToRecruit.transform.parent = null;
        Destroy(gameObject);
    }
}
