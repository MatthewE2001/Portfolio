using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotation = 1.0f;
    public float jumpStrength = 5.0f;

    public float rotationX;
    public float rotationY;
    public float maxRotY;
    public float minRotY;
    public float turnSpeed = 0.0f;
    public Camera playerCamera;

    public GameObject exploreTotem;
    bool totemCooldownActive = false;
    public float wait = 5.0f;
    Vector3 lastSpawnPosition = Vector3.zero;
    float playerRotateValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        moveSpeed = GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayerAndCamera();
        CheckGolemSwap();
    }

    private void FixedUpdate()
    {
        if (totemCooldownActive == false)
        {
            InstantiateTotem();
        }

        if (totemCooldownActive == true)
        {
            wait -= Time.deltaTime;
            Debug.Log(wait);

            if (wait <= 0.0f)
            {
                totemCooldownActive = false;
                wait = 4.0f;
                Debug.Log("Full Reset");
            }
        }
    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
            playerRotateValue -= 1.0f;
            transform.eulerAngles = new Vector3(0, playerRotateValue, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
            playerRotateValue += 1.0f;
            transform.eulerAngles = new Vector3(0, playerRotateValue, 0);
        }
    }

    void RotatePlayerAndCamera()
    {
        //rotationX += Input.GetAxis("Mouse Y") * turnSpeed;
        float tmpY = Input.GetAxis("Mouse X") * turnSpeed;
        //rotationX = Mathf.Clamp(rotationX, minRotY, maxRotY);

        playerCamera.transform.eulerAngles = new Vector3(0, playerCamera.transform.eulerAngles.y + tmpY, 0);
        //tmpY = -tmpY;
        //transform.eulerAngles = new Vector3(0, (transform.eulerAngles.y + tmpY), 0);
    }

    void CheckGolemSwap()
    {
        //if (GameObject.Find("GameManager").GetComponent<ChangeGolems>().GolemStatus() == true)
        //{
            moveSpeed = GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetSpeed();
            //GameObject.Find("GameManager").GetComponent<ChangeGolems>().ResetGolemChanged();
        //}
    }

    void InstantiateTotem()
    {
        if (Input.GetKey(KeyCode.Mouse1) && GameObject.Find("GameManager").GetComponent<ChangeGolems>().GetActiveGolem() == 1)
        {
            Vector3 tmpVector = new Vector3(this.transform.position.x + 1, this.transform.position.y -1, this.transform.position.z + 1);

            if (lastSpawnPosition != tmpVector)
            {
                GameObject lightTorch = Instantiate(exploreTotem, tmpVector, this.transform.rotation);
                DontDestroyOnLoad(lightTorch); // Added by Nick on 2/26 @ 8:28am in order ensure lights stayed in the scene during figure swaps
                Debug.Log("torchPlaced");
                totemCooldownActive = true;
                lastSpawnPosition = tmpVector;
            }
        }
    }
}
