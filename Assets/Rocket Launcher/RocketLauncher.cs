using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    //Public Numerical Values
    public float avoidingSpeed = 5;
    public float adsFovModifier = 45;
    public float coolDown = 2;
    //Public Object References
    public GameObject mainCam;
    public GameObject playerBody;
    public GameObject rifleTip;
    public GameObject rocketProjectile;
    public GameObject rocketModel;
    public Transform rocketPosition;
    public Transform adsPos;
    public ParticleSystem smokeParticle;
    //Private Variables
    private float recoilTimed = 0;
    private float viewModelTime;
    private float adsTime = 0;
    private float viewBobTime = 0f;
    private bool viewBobDirection = true;
    private bool colliding = false;
    private bool onCooldown = false;
    private bool recoiling = false;
    private Vector3 startingPos;
    Quaternion randomAngle;
    Quaternion randomAngle2;

    private void OnEnable()
    {
        if (onCooldown)
        {
            StartCoroutine(CooldownTimer());
        }
    }
    public void Awake()
    {
        startingPos = transform.localPosition;
    }

    void Update()
    {
        //Rocket firing + Recoil Trigger
        if (Input.GetKeyDown(KeyCode.Mouse0) && !onCooldown)
        {
            smokeParticle.Play();
            rocketModel.GetComponent<MeshRenderer>().enabled = false;
            onCooldown = true;
            StartCoroutine(CooldownTimer());
            Instantiate(rocketProjectile, rocketPosition.position, rocketPosition.transform.rotation);
            StartCoroutine(RecoilTimer());
            randomAngle = Quaternion.Euler(mainCam.transform.localRotation.eulerAngles + new Vector3(-30, 0, 0));
            randomAngle2 = Quaternion.Euler(playerBody.transform.localRotation.eulerAngles + new Vector3(0, 20, 0));

        }
        //Recoil
        if (recoiling)
        {
            mainCam.transform.localRotation = Quaternion.RotateTowards(mainCam.transform.localRotation, randomAngle, 0.2f);
            playerBody.transform.localRotation = Quaternion.RotateTowards(playerBody.transform.localRotation, randomAngle2, 0.2f);
        }
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
    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        onCooldown = false;
        rocketModel.GetComponent<MeshRenderer>().enabled = true;

    }
    IEnumerator RecoilTimer()
    {
        recoiling = true;
        yield return new WaitForSeconds(0.05f);
        recoiling = false;
    }

}
