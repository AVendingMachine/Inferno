using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Transform rifleTip;
    public LayerMask bulletMask;
    public GameObject bulletMarker;
    private Vector3 deviation;
    public float spread = 0.1f;
    private float coolDown = 0f;
    public float fireCoolDown = 0.5f;
    public float recoilSpeedTimer;
    //public float recoilSpeed = 1;
    private Vector3 lookPoint;
    
    void Update()
    {
       
        coolDown = Mathf.Clamp(coolDown - Time.deltaTime, 0, fireCoolDown);
        RaycastHit hit;
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookPoint - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, recoilSpeedTimer);
            recoilSpeedTimer += fireCoolDown * Time.deltaTime*100;
            if (coolDown <= 0)
            {
                recoilSpeedTimer = 0;
                Shoot();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            deviation = new Vector3(0, 0, 0);
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
       
        void Shoot()
        {
            coolDown = fireCoolDown;
            deviation = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
            if (Physics.Raycast(rifleTip.position, rifleTip.forward + deviation, out hit, Mathf.Infinity, bulletMask))
            {
                Instantiate(bulletMarker, hit.point, Quaternion.identity);
                lookPoint = hit.point;
            }

       
            Debug.DrawRay(rifleTip.position, rifleTip.forward + deviation, Color.red, 1);
        }
    }
}
