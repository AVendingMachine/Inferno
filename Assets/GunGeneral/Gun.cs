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
    public float reloadTime = 2f;
    public float recoilMod = 0.5f;
    public int maxAmmo = 20;
    public int bulletsAShot = 1;
    //Public Object References
    public GameObject bulletMarker;
    public GameObject mainCam;
    public GameObject playerBody;
    public GameObject rifleTip;
    public GameObject muzzleFlash;
    public GameObject magazine;
    public Transform adsPos;
    public Transform magPos;
    public LayerMask bulletMask;
    //Private Variables
    private float currentSpread;
    private float coolDown = 0f;
    private int currentammo;
    private float viewModelTime;
    private float adsTime = 0;
    private float viewBobTime = 0f;
    private bool viewBobDirection = true;
    private bool recoilEnabled = false;
    private bool colliding = false;
    private bool reloading = false;
    private Vector3 deviation;
    private Vector3 startingPos;
    private Quaternion startingRot;
    private Quaternion recoilAngle;

    private void Awake()
    {
        currentammo = maxAmmo;
    }
    private void OnEnable()
    {
        if (currentammo <= 0)
        {
            StartCoroutine(Reload());
        }

    }
    public void Start()
    {
        currentSpread = spread;
        startingPos = transform.localPosition;
        startingRot = transform.localRotation;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && currentammo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            if (!reloading)
            {
                StartCoroutine(Reload());
            }
            
        }
        //Viewbob
        if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1) && colliding == false && playerBody.GetComponent<PlayerMovement>().isMoving == true)
        {
            if (viewBobDirection == true)
            {
                transform.localPosition += Vector3.up*Time.deltaTime*0.1f;
                viewBobTime = Mathf.Clamp(viewBobTime+Time.deltaTime*5,0,1);
            }
            if (viewBobDirection == false)
            {
                transform.localPosition -= Vector3.up*Time.deltaTime*0.1f;
                viewBobTime = Mathf.Clamp(viewBobTime - Time.deltaTime*5, 0, 1);
            }
            if (viewBobTime >=1)
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
            
            
            if (coolDown <= 0 && currentammo > 0 &! reloading)
            {
                recoilAngle = Quaternion.Euler(-fireCoolDown*recoil, fireCoolDown*recoil, -fireCoolDown*recoil);
                Shoot();
                StartCoroutine(MuzzleFlash());
                StartCoroutine(Recoil(coolDown));
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }

        }
        if (recoilEnabled)
        {
            Quaternion randomAngle = Quaternion.Euler(mainCam.transform.localRotation.eulerAngles + new Vector3(Random.Range(-30,10), 0, 0));
            mainCam.transform.localRotation = Quaternion.RotateTowards(mainCam.transform.localRotation, randomAngle, coolDown);
            Quaternion randomAngle2 = Quaternion.Euler(playerBody.transform.localRotation.eulerAngles + new Vector3(0, Random.Range(-10, 20), 0));
            playerBody.transform.localRotation = Quaternion.RotateTowards(playerBody.transform.localRotation, randomAngle2, coolDown);
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
            currentammo = Mathf.Clamp(currentammo - bulletsAShot, 0, maxAmmo);
            coolDown = fireCoolDown;
            deviation = new Vector3(Random.Range(-currentSpread, currentSpread), Random.Range(-currentSpread, currentSpread), Random.Range(-currentSpread, currentSpread));
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward + deviation, out hit, Mathf.Infinity, bulletMask))
            {
                Instantiate(bulletMarker, hit.point, Quaternion.identity);
            }
        }
 
    }


    IEnumerator Reload()
    {
        reloading = true;
        //Vector3 magStartPos = magazine.transform.localPosition;
        //Quaternion magStartRot = magazine.transform.localRotation;
        magazine.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(reloadTime);
        magazine.GetComponent<Rigidbody>().isKinematic = true;
        currentammo = maxAmmo;
        magazine.transform.localPosition = magPos.localPosition;
        magazine.transform.localRotation = magPos.localRotation;
        reloading = false;
    }
    IEnumerator Recoil(float timer)
    {
        recoilEnabled = true;
        yield return new WaitForSeconds(recoilMod*timer);
        recoilEnabled = false; 
    }
    IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.04f);
        muzzleFlash.SetActive(false);
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
