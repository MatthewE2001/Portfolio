using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrder : MonoBehaviour
{
    int flavorOmega;
    int flavorAlpha;
    int flavorSigma;
    int flavorTheta;

    public Transform holdingItemPosition;

    Sprite happySprite;
    Sprite unhappySprite;

    int numWantedFlavors = 0;
    int numNeutralFlavors = 0;
    int numUnwantedFlavors = 0;
    int resultHappiness = 0;

    bool alreadyGrabbed = false;

    int[] wantedSum = { 0, 1, 4, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
    int[] unwantedSum = { 0, 1, 3, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

    void Start()
    {
    }

    private void Awake()
    {

    }

    void Update()
    {
        
    }

    public void SetFlavors(int alpha, int omega, int sigma, int theta)
    {
        flavorAlpha = alpha;
        flavorOmega = omega;
        flavorSigma = sigma;
        flavorTheta = theta;
    }

    public void SetSprites(Sprite happy, Sprite sad)
    {
        happySprite = happy;
        unhappySprite = sad;
    }

    public int GetAlpha()
    {
        return flavorAlpha;
    }

    public int GetOmega()
    {
        return flavorOmega;
    }

    public int GetTheta()
    {
        return flavorTheta;
    }

    public int GetSigma()
    {
        return flavorSigma;
    }

    void CompareFoodItemToWants(GrabbableItemScript itemScript)
    {
        switch (flavorAlpha)
        {
            case -1:
                numUnwantedFlavors += itemScript.flavorAlpha;
                break;
            case 0:
                numNeutralFlavors += itemScript.flavorAlpha;
                break;
            case 1:
                numWantedFlavors += itemScript.flavorAlpha;
                break;
        }
        switch (flavorOmega)
        {
            case -1:
                numUnwantedFlavors += itemScript.flavorOmega;
                break;
            case 0:
                numNeutralFlavors += itemScript.flavorOmega;
                break;
            case 1:
                numWantedFlavors += itemScript.flavorOmega;
                break;
        }
        switch (flavorSigma)
        {
            case -1:
                numUnwantedFlavors += itemScript.flavorSigma;
                break;
            case 0:
                numNeutralFlavors += itemScript.flavorSigma;
                break;
            case 1:
                numWantedFlavors += itemScript.flavorSigma;
                break;
        }
        switch (flavorTheta)
        {
            case -1:
                numUnwantedFlavors += itemScript.flavorTheta;
                break;
            case 0:
                numNeutralFlavors += itemScript.flavorTheta;
                break;
            case 1:
                numWantedFlavors += itemScript.flavorTheta;
                break;
        }

        resultHappiness = wantedSum[numWantedFlavors] - unwantedSum[numUnwantedFlavors] - numNeutralFlavors;
        //Debug.Log("Wanted " + wantedSum[numWantedFlavors]);
        //Debug.Log("Unwanted " + unwantedSum[numUnwantedFlavors]);
        //Debug.Log("Neutral " + numNeutralFlavors);
        //Debug.Log("Result " + resultHappiness);
    }

    void ChangeSpriteBasedOnHappiness()
    {
        if (resultHappiness >= 0)
            GetComponent<SpriteRenderer>().sprite = happySprite;
        else
            GetComponent<SpriteRenderer>().sprite = unhappySprite;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!alreadyGrabbed && collision.gameObject.GetComponent<GrabbableItemScript>() != null)
            if(collision.gameObject.GetComponent<GrabbableItemScript>().canHaveFlavor)
            {
                alreadyGrabbed = true;

                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                collision.transform.position = holdingItemPosition.position;
                collision.gameObject.transform.SetParent(this.transform);

                CompareFoodItemToWants(collision.gameObject.GetComponent<GrabbableItemScript>());
                ChangeSpriteBasedOnHappiness();

                GameManager.Instance.AddApproval(resultHappiness);
                GetComponent<CustomerMovement>().StartLeavingAfterDelay();
            }
    }
}