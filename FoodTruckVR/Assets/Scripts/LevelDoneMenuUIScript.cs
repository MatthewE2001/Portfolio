using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class LevelDoneMenuUIScript : MonoBehaviour
{
    public enum MenuButtonType
    {
        NextDay,
        BackToMainMenu,

        NumOfTypes
    };

    [Header("Buttons")]
    public HoverButton nextDayButton;
    public HoverButton backToMainMenuButton;

    [Header("Texts")]
    public TextMesh resultText;
    public TextMesh finalApprovalText;
    public TextMesh pointsDeductedText;

    // Start is called before the first frame update
    void Start()
    {
        nextDayButton.onButtonDown.AddListener(delegate { OnButtonDown(MenuButtonType.NextDay); });
        backToMainMenuButton.onButtonDown.AddListener(delegate { OnButtonDown(MenuButtonType.BackToMainMenu); });
    }

    void Update()
    {
        finalApprovalText.text = "Final Approval: " + GameManager.Instance.currentApproval
            + "/" + GameManager.Instance.maxApproval;

        if (GameManager.Instance.currentApproval == GameManager.Instance.maxApproval)
        {
            pointsDeductedText.text = "Points Deducted (0%): " + 0;
            resultText.text = "Well Done!";
        }
        else
        {
            pointsDeductedText.text = "Points Deducted (20%): " + GameManager.Instance.currentApproval / 5;
            resultText.text = "Keep working!";
        }
    }

    public void OnButtonDown(MenuButtonType buttonType)
    {
        switch (buttonType)
        {
            case MenuButtonType.NextDay:
                {
                    nextDayButton.GetComponent<ButtonFeedback>().OnButtonDown();
                    GameManager.Instance.StartNextDay();
                    break;
                }
            case MenuButtonType.BackToMainMenu:
                {
                    backToMainMenuButton.GetComponent<ButtonFeedback>().OnButtonDown();
                    SceneManagerScript.Instance.LoadLevel(SceneManagerScript.Levels.MainMenu);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
