using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    //Public Numerical Values
    public float avoidingSpeed = 5;
    public float adsFovModifier = 45;
    //Public Object References
    public GameObject mainCam;
    public GameObject playerBody;
    public GameObject rifleTip;
    public Transform adsPos;
    //Private Variables
    private float viewModelTime;
    private float adsTime = 0;
    private float viewBobTime = 0f;
    private bool viewBobDirection = true;
    private bool colliding = false;
    private Vector3 startingPos;
    public void Start()
    {
        startingPos = transform.localPosition;
    }

    void Update()
    {
        //Viewbob
        if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1) && colliding == false && playerBody.GetComponent<PlayerMovement>().isMoving == true)
        {
            if (viewBobDirection == true)
            {
                transform.localPosition += Vector3.up * Time.deltaTime * 0.1f;
                viewBobTime = Mathf.Clamp(viewBobTime + Time.deltaTime * 5, 0, 1);
            }
            if (viewBobDirection == false)
            {
                transform.localPosition -= Vector3.up * Time.deltaTime * 0.1f;
                viewBobTime = Mathf.Clamp(viewBobTime - Time.deltaTime * 5, 0, 1);
            }
            if (viewBobTime >= 1)
            {
                viewBobDirection = false;
            }
            if (viewBobTime <= 0)
            {
                viewBobDirection = true;
            }

        }
        else
        {
            viewBobDirection = true;
            viewBobTime = 0;
        }
        //ADS handler
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerBody.GetComponent<PlayerMovement>().aimingDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            transform.localPosition = startingPos;
            playerBody.GetComponent<PlayerMovement>().aimingDown = false;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            adsTime = Mathf.Clamp(adsTime + Time.deltaTime * 10, 0, 1);
        }
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            adsTime = Mathf.Clamp(adsTime - Time.deltaTime * 10, 0, 1);
        }
        if (adsTime != 0)
        {
            transform.localPosition = Vector3.Lerp(startingPos, adsPos.localPosition, adsTime);
        }

        mainCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(60, adsFovModifier, adsTime);

    }
    ///This section handles the view model bump effect, and was from a previous script 
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            colliding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            colliding = false;
        }
    }
    private void FixedUpdate()
    {
        if (colliding)
        {
            transform.localPosition -= new Vector3(0, 0, avoidingSpeed * Time.deltaTime);
            Debug.Log("Hello everybody my name is Markiplier");
            viewModelTime = 0;
        }
        if (!colliding && rifleTip.GetComponent<GunTipCollider>().colliding == false)
        {
            viewModelTime = Mathf.Clamp(viewModelTime + Time.deltaTime, 0, 1);
            if (adsTime == 0 && transform.localPosition.z != startingPos.z)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, startingPos, viewModelTime);
            }
        }
    }
}
