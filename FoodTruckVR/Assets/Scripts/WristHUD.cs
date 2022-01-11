using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristHUD : MonoBehaviour
{
    // Start is called before the first frame update
    Transform leftHand;
    //public TextMesh textMesh;

    public TextMesh HUDText;
    public TextMesh alphaText;
    public TextMesh omegaText;
    public TextMesh sigmaText;
    public TextMesh thetaText;

    public void Initialize()
    {
        leftHand = GameObject.Find("LeftHand").transform;
    }

    public void UpdateHUDPosition()
    {
        transform.position = leftHand.position;
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    public void UpdateHUD(string itemName, bool isFried, int timesInEnlarger, bool canHaveFlavor,
        int alpha, int omega, int sigma, int theta)
    {
        HUDText.text = itemName + "\n";

        if (canHaveFlavor)
        {
            if(isFried)
                HUDText.text += "Fried" + "\n";
            else
                HUDText.text += "Not fried" + "\n";

            alphaText.text = alpha.ToString();
            omegaText.text = omega.ToString();
            sigmaText.text = sigma.ToString();
            thetaText.text = theta.ToString();
        }
        else
        {
            alphaText.text = "0";
            omegaText.text = "0";
            sigmaText.text = "0";
            thetaText.text = "0";
        }

        HUDText.text += "Enlarged " + timesInEnlarger + " times" + "\n";
    }
}
