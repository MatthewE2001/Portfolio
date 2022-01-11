using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSystem : MonoBehaviour
{
    [System.Serializable]
    public struct Customer
    {
        public Sprite customerSprite;
        public Sprite happySprite;
        public Sprite unhappySprite;

        public Transform spawnPosition;
        public Transform foodTruckPosition;
        public Transform leavePosition;

        [Header("-1 = doesn't want, 0 - neutral, 1 - wants")]
        public int flavorOmega;
        public int flavorAlpha;
        public int flavorSigma;
        public int flavorTheta;
    }

    public GameObject shutter;

    public TextMesh ordersCounterText;
    int currentCustomerIndex = -1;

    public GameObject customerPrefab;
    public Customer[] customersDataArray;

    GameObject ServingTablet;

    void Start()
    {
        ServingTablet = GameObject.Find("ServingTablet");

        NewDayStart();
    }

    public void NewDayStart()
    {
        shutter.GetComponent<ShutterScript>().MoveToPosition(true, false);
        currentCustomerIndex = -1;
        SpawnNewCustomer();
    }    

    public void SpawnNewCustomer()
    {
        currentCustomerIndex++;

        if (currentCustomerIndex >= customersDataArray.Length)
        {
            currentCustomerIndex = customersDataArray.Length;
            shutter.GetComponent<ShutterScript>().MoveToPosition(false, true);
        }
        else
        {
            GameObject customer = Instantiate(customerPrefab);

            customer.GetComponent<SpriteRenderer>().sprite = customersDataArray[currentCustomerIndex].customerSprite;

            customer.GetComponent<CustomerOrder>().SetFlavors(
                customersDataArray[currentCustomerIndex].flavorAlpha,
                customersDataArray[currentCustomerIndex].flavorOmega,
                customersDataArray[currentCustomerIndex].flavorSigma,
                customersDataArray[currentCustomerIndex].flavorTheta);

            customer.GetComponent<CustomerOrder>().SetSprites(
                customersDataArray[currentCustomerIndex].happySprite,
                customersDataArray[currentCustomerIndex].unhappySprite);

            customer.GetComponent<CustomerMovement>().SetPositions(
                customersDataArray[currentCustomerIndex].spawnPosition,
                customersDataArray[currentCustomerIndex].foodTruckPosition,
                customersDataArray[currentCustomerIndex].leavePosition);

            customer.GetComponent<CustomerMovement>().StartMovingToFoodTruck();
            ServingTablet.GetComponent<OrderHUD>().SetCurrentCustomer(customer);
        }

        UpdateCustomersCounter();
    }

    void UpdateCustomersCounter()
    {
        ordersCounterText.text = "Orders Left: " + (customersDataArray.Length - currentCustomerIndex);
    }
}
