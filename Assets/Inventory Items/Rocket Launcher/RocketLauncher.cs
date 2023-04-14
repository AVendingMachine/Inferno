using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    //Public Numerical Values
    public float adsFovModifier = 45;
    public float coolDown = 2;
    //Public Object References
    public GameObject mainCam;
    public GameObject playerBody;
    public GameObject rocketProjectile;
    public GameObject rocketModel;
    public Transform rocketPosition;
    public Transform adsPos;
    public ParticleSystem smokeParticle;
    //Private Variables
    private float adsTime = 0;
    private bool reloading = false;
    private bool recoiling = false;
    private Vector3 startingPos;
    Quaternion randomAngle;
    Quaternion randomAngle2;

    private void OnEnable()
    {
       // if (reloading)
       // {
       //     StartCoroutine(CooldownTimer());
     //   }
    }
    public void Awake()
    {
        startingPos = transform.localPosition;
    }

    void Update()
    {
        if (GetComponent<AmmoSystem>().currentAmmo <= 0 && !reloading && InGameMenu.gamePaused == false)
        {
            rocketModel.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(Reload());
        }
        //Rocket firing + Recoil Trigger
        if (Input.GetKeyDown(KeyCode.Mouse0) && !reloading && InGameMenu.gamePaused == false)
        {
            GetComponent<AmmoSystem>().LoseAmmo(1);
            smokeParticle.Play();
            
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


    private IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(GetComponent<AmmoSystem>().reloadTime);
        reloading = false;
        rocketModel.GetComponent<MeshRenderer>().enabled = true;
    }
    IEnumerator RecoilTimer()
    {
        recoiling = true;
        yield return new WaitForSeconds(0.05f);
        recoiling = false;
    }

}
