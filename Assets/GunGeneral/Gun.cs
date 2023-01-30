using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Public Numerical Values
    public float spread = 0.1f;
    public float fireCoolDown = 0.5f;
    public float gunShake = 1f;
    public float recoil = 100f;
    public float adsStabilityMod = 0.3f;
    public float avoidingSpeed = 5;
    public float adsFovModifier = 45;
    //Public Object References
    public GameObject bulletMarker;
    public GameObject mainCam;
    public GameObject playerBody;
    public GameObject rifleTip;
    public Transform adsPos;
    public LayerMask bulletMask;
    //Private Variables
    private float currentSpread;
    private float coolDown = 0f;
    private float viewModelTime;
    private float adsTime = 0;
    private bool recoilEnabled = false;
    private bool colliding = false;
    private Vector3 deviation;
    private Vector3 startingPos;
    private Quaternion startingRot;
    private Quaternion recoilAngle;


    public void Start()
    {
        currentSpread = spread;
        startingPos = transform.localPosition;
        startingRot = transform.localRotation;
    }

    void Update()
    {
        //ADS handler
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentSpread = adsStabilityMod * spread;
            playerBody.GetComponent<PlayerMovement>().aimingDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            currentSpread = spread;
            transform.localPosition = startingPos;
            playerBody.GetComponent<PlayerMovement>().aimingDown = false;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            adsTime = Mathf.Clamp(adsTime + Time.deltaTime*10, 0, 1);
        }
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            adsTime = Mathf.Clamp(adsTime - Time.deltaTime*10, 0, 1);
        }
        if (adsTime != 0)
        {
            transform.localPosition = Vector3.Lerp(startingPos, adsPos.localPosition, adsTime);
        }
        
        mainCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(60, adsFovModifier, adsTime);


        //Recoil Control
        transform.localRotation = Quaternion.Lerp(startingRot, recoilAngle, coolDown);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            
            
            if (coolDown <= 0)
            {
                recoilAngle = Quaternion.Euler(-fireCoolDown*recoil, fireCoolDown*recoil, -fireCoolDown*recoil);
                Shoot();
                StartCoroutine(Recoil(coolDown));
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }

        }
        if (recoilEnabled)
        {
            Quaternion randomAngle = Quaternion.Euler(mainCam.transform.localRotation.eulerAngles + new Vector3(Random.Range(-40,1), 0, 0));
            mainCam.transform.localRotation = Quaternion.RotateTowards(mainCam.transform.localRotation, randomAngle, coolDown);
            //Quaternion randomAngleTwo = Quaternion.Euler(mainCam.transform.localRotation.eulerAngles + new Vector3(0, Random.Range(-40, 1), 0));
            //playerBody.transform.rotation = Quaternion.RotateTowards(playerBody.transform.rotation, randomAngleTwo, 0.5f);
        }
        //Handles the cooldown and the spread modifier, as well as the bullet markers (to be removed)
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            deviation = new Vector3(0, 0, 0);
        }
        coolDown = Mathf.Clamp(coolDown - Time.deltaTime, 0, fireCoolDown);
        RaycastHit hit;
        void Shoot()
        {
            coolDown = fireCoolDown;
            deviation = new Vector3(Random.Range(-currentSpread, currentSpread), Random.Range(-currentSpread, currentSpread), Random.Range(-currentSpread, currentSpread));
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward + deviation, out hit, Mathf.Infinity, bulletMask))
            {
                Instantiate(bulletMarker, hit.point, Quaternion.identity);
            }
        }
 
    }

    IEnumerator Recoil(float timer)
    {
        recoilEnabled = true;
        yield return new WaitForSeconds(0.1f*timer);
        recoilEnabled = false; 
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
            if (adsTime == 0)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, startingPos, viewModelTime);
            }
        }
    }
}
