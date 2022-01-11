using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderHUD : MonoBehaviour
{
    [Header("General Public Variables")]
    public Transform tablet; //location for where the HUD should appear
    public GameObject currentCustomer;

    [Header("Want/Unwanted Flavor Symbols")]
    public GameObject alpha;
    public GameObject alphaUnwanted;
    public GameObject omega;
    public GameObject omegaUnwanted;
    public GameObject sigma;
    public GameObject sigmaUnwanted;
    public GameObject theta;
    public GameObject thetaUnwanted;

    //textMesh on the serving object for designers to add text if wanted

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this could possibly be changed from public
    public void ChangeCustomerInformation()
    {
        if(currentCustomer.GetComponent<CustomerOrder>().GetAlpha() >0)
        {
            alpha.SetActive(true);
        }
        if (currentCustomer.GetComponent<CustomerOrder>().GetOmega() > 0)
        {
            omega.SetActive(true);
        }
        if (currentCustomer.GetComponent<CustomerOrder>().GetSigma() > 0)
        {
            sigma.SetActive(true);
        }
        if (currentCustomer.GetComponent<CustomerOrder>().GetTheta() > 0)
        {
            theta.SetActive(true);
        }

        if (currentCustomer.GetComponent<CustomerOrder>().GetAlpha() < 0)
        {
            alphaUnwanted.SetActive(true);
        }
        if (currentCustomer.GetComponent<CustomerOrder>().GetOmega() < 0)
        {
            omegaUnwanted.SetActive(true);
        }
        if (currentCustomer.GetComponent<CustomerOrder>().GetSigma() < 0)
        {
            sigmaUnwanted.SetActive(true);
        }
        if (currentCustomer.GetComponent<CustomerOrder>().GetTheta() < 0)
        {
            thetaUnwanted.SetActive(true);
        }
    }

    public void SetCurrentCustomer(GameObject newCustomer)
    {
        currentCustomer = newCustomer;

        alpha.SetActive(false);
        omega.SetActive(false);
        sigma.SetActive(false);
        theta.SetActive(false);

        alphaUnwanted.SetActive(false);
        omegaUnwanted.SetActive(false);
        sigmaUnwanted.SetActive(false);
        thetaUnwanted.SetActive(false);

        ChangeCustomerInformation();
    }
}