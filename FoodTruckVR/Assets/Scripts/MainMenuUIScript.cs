using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class MainMenuUIScript : MonoBehaviour
{
    public enum MenuButtonType
    {
        CookingTime,
        Options,

        BackFromOptions,

        PlanetOne,
        PlanetTwo,
        BackFromPlanets,

        NumOfTypes
    };

    public enum CanvasType
    {
        MainCanvas,
        ChoosePlanetCanvas,
        OptionsCanvas,
        NumOfTypes
    };

    [Header("Buttons")]
    public HoverButton CookingTimeButton;
    public HoverButton OptionsButton;
    public HoverButton BackFromOptionsButton;
    public HoverButton PlanetOneButton;
    public HoverButton PlanetTwoButton;
    public HoverButton BackFromPlanetsButton;

    [Header("Canvases")]
    public GameObject mainCanvas;
    public GameObject optionsCanvas;
    public GameObject choosePlanetCanvas;
    private CanvasType currentCanvas;

    [Header("UI Positions")]
    public Transform activeCanvasPositionTransform;
    public Transform inactiveCanvasPositionTransform;
    public float uiMovementSpeed;

    [Header("Projector Staff")]
    public GameObject menuBackground;
    public HoverButton projectorButton;
    public GameObject projectorLight;
    bool isMenuActive = true;

    // Start is called before the first frame update
    void Start()
    {
        currentCanvas = CanvasType.MainCanvas;

        CookingTimeButton.onButtonDown.AddListener(delegate { OnButtonDown(MenuButtonType.CookingTime); });
        OptionsButton.onButtonDown.AddListener(delegate { OnButtonDown(MenuButtonType.Options); });
        BackFromOptionsButton.onButtonDown.AddListener(delegate { OnButtonDown(MenuButtonType.BackFromOptions); });
        PlanetOneButton.onButtonDown.AddListener(delegate { OnButtonDown(MenuButtonType.PlanetOne); });
        PlanetTwoButton.onButtonDown.AddListener(delegate { OnButtonDown(MenuButtonType.PlanetTwo); });
        BackFromPlanetsButton.onButtonDown.AddListener(delegate { OnButtonDown(MenuButtonType.BackFromPlanets); });

        projectorButton.onButtonDown.AddListener(ToggleProjector);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleProjector(Hand hand)
    {
        isMenuActive = !isMenuActive;

        if (isMenuActive)
        {
            menuBackground.SetActive(true);
            mainCanvas.SetActive(true);
            optionsCanvas.SetActive(true);
            choosePlanetCanvas.SetActive(true);
            projectorLight.SetActive(true);
        }
        else
        {
            menuBackground.SetActive(false);
            mainCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            choosePlanetCanvas.SetActive(false);
            projectorLight.SetActive(false);
        }
    }

    void LoadCanvas(CanvasType canvasType)
    {
        if (currentCanvas != canvasType)
        {
            mainCanvas.GetComponent<MainMenuCanvasScript>().
                LerpToPosition(inactiveCanvasPositionTransform, uiMovementSpeed);
            optionsCanvas.GetComponent<MainMenuCanvasScript>().
                LerpToPosition(inactiveCanvasPositionTransform, uiMovementSpeed);
            choosePlanetCanvas.GetComponent<MainMenuCanvasScript>().
                LerpToPosition(inactiveCanvasPositionTransform, uiMovementSpeed);

            switch (canvasType)
            {
                case CanvasType.MainCanvas:
                    {
                        mainCanvas.GetComponent<MainMenuCanvasScript>().
                            LerpToPosition(activeCanvasPositionTransform, uiMovementSpeed);
                        break;
                    }
                case CanvasType.OptionsCanvas:
                    {
                        optionsCanvas.GetComponent<MainMenuCanvasScript>().
                            LerpToPosition(activeCanvasPositionTransform, uiMovementSpeed);
                        break;
                    }
                case CanvasType.ChoosePlanetCanvas:
                    {
                        choosePlanetCanvas.GetComponent<MainMenuCanvasScript>().
                            LerpToPosition(activeCanvasPositionTransform, uiMovementSpeed);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            currentCanvas = canvasType;
        }
    }

    public void OnButtonDown(MenuButtonType buttonType)
    {
        switch (buttonType)
        {
            case MenuButtonType.CookingTime:
                {
                    CookingTimeButton.GetComponent<ButtonFeedback>().OnButtonDown();
                    LoadCanvas(CanvasType.ChoosePlanetCanvas);
                    break;
                }
            case MenuButtonType.Options:
                {
                    OptionsButton.GetComponent<ButtonFeedback>().OnButtonDown();
                    LoadCanvas(CanvasType.OptionsCanvas);
                    break;
                }
            case MenuButtonType.BackFromOptions:
                {
                    BackFromOptionsButton.GetComponent<ButtonFeedback>().OnButtonDown();
                    LoadCanvas(CanvasType.MainCanvas);
                    break;
                }
            case MenuButtonType.PlanetOne:
                {
                    PlanetOneButton.GetComponent<ButtonFeedback>().OnButtonDown();
                    SceneManagerScript.Instance.LoadLevel(SceneManagerScript.Levels.PlanetOne);
                    break;
                }
            case MenuButtonType.PlanetTwo:
                {
                    PlanetTwoButton.GetComponent<ButtonFeedback>().OnButtonDown();
                    break;
                }
            case MenuButtonType.BackFromPlanets:
                {
                    BackFromPlanetsButton.GetComponent<ButtonFeedback>().OnButtonDown();
                    LoadCanvas(CanvasType.MainCanvas);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
