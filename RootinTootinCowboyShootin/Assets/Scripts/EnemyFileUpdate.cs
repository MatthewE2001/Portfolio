using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using System;

public class EnemyFileUpdate : MonoBehaviour
{
    public TextAsset dataDrivenInput;
    [Header("AI Values, ONLY to be used during play")]
    public float doingDamageModifier;
    public float takingDamageModifier;
    public float allyModifier;
    public float allyRange;
    public float stayCloseMultiplier;
    public float stayCloseRange;
    [Range(0.0f, 100.0f)]
    public float difficultyRating;
    // Start is called before the first frame update
    void Start()
    {
        string[] fileInput = dataDrivenInput.text.Split('\n');
        
        for (int i = 0; i < fileInput.Length; i++)
        {
            if(fileInput[i].Substring(fileInput[i].Length-1) == "\r")
            {
                fileInput[i] = fileInput[i].Substring(0, fileInput[i].Length - 1);
            }
            switch(fileInput[i].Split(' ')[0])
            {
                case "doingDamageModifier":
                    doingDamageModifier = float.Parse(fileInput[i].Split(' ')[1]);
                    break;
                case "takingDamageModifier":
                    takingDamageModifier = float.Parse(fileInput[i].Split(' ')[1]);
                    break;
                case "allyModifier":
                    allyModifier = float.Parse(fileInput[i].Split(' ')[1]);
                    break;
                case "allyRange":
                    allyRange = float.Parse(fileInput[i].Split(' ')[1]);
                    break;
                case "stayCloseMultiplier":
                    stayCloseMultiplier = float.Parse(fileInput[i].Split(' ')[1]);
                    break;
                case "stayCloseRange":
                    stayCloseRange = float.Parse(fileInput[i].Split(' ')[1]);
                    break;
                case "difficultyRating":
                    difficultyRating = float.Parse(fileInput[i].Split(' ')[1]);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
