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
    public float adsFovModifier = 45;
    //public float reloadTime = 2f;
    public float recoilMod = 0.5f;
    public float damage = 1;
    public float maxAmmo = 20;
    public int bulletsAShot = 1;
    //Public Object References
    public GameObject bulletMarker;
    public GameObject mainCam;
    public GameObject playerBody;
    public GameObject muzzleFlash;
    public GameObject magazine;
    public Transform adsPos;
    public Transform magPos;
    public LayerMask bulletMask;
    //Private Variables
    private float currentSpread;
    private float coolDown = 0f;
    public float currentammo;
    private float adsTime = 0;
    private bool recoilEnabled = false;
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


            if (coolDown <= 0 && GetComponent<AmmoSystem>().currentAmmo > 0 && !reloading && InGameMenu.gamePaused == false)
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
            //currentammo = Mathf.Clamp(currentammo - bulletsAShot, 0, maxAmmo);
            currentammo = GetComponent<AmmoSystem>().currentAmmo;
            GetComponent<AmmoSystem>().LoseAmmo(bulletsAShot);
            
            coolDown = fireCoolDown;
            deviation = new Vector3(Random.Range(-currentSpread, currentSpread), Random.Range(-currentSpread, currentSpread), Random.Range(-currentSpread, currentSpread));
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward + deviation, out hit, Mathf.Infinity, bulletMask))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
                    Debug.Log("hit enemy :]");
                }
            }
        }
 
    }


    IEnumerator Reload()
    {
        if (GetComponent<AmmoSystem>().outOfAmmo == false)
        {
            reloading = true;
            magazine.GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForSeconds(GetComponent<AmmoSystem>().reloadTime);
            magazine.GetComponent<Rigidbody>().isKinematic = true;
            //if (GetComponent<AmmoSystem>().currentReserve >= maxAmmo) { 
            // }
            currentammo = Mathf.Clamp(GetComponent<AmmoSystem>().currentReserve,0,maxAmmo);
            magazine.transform.localPosition = magPos.localPosition;
            magazine.transform.localRotation = magPos.localRotation;
            reloading = false;
        }
        
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
}
