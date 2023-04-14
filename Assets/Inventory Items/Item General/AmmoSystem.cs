using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    public float maxAmmo = 30;
    public float maxReserve = 240;
    public float currentAmmo;
    public float currentReserve;
    public float reloadTime = 2;
    public bool outOfAmmo = false;
    bool reloading = false;

    private void Awake()
    {
        currentAmmo = maxAmmo;
        currentReserve = maxReserve;
    }
    private void OnEnable()
    {
        
        if (currentAmmo == 0)
        {
            StartCoroutine(Refill());
        }
    }
    void Start()
    {
        
        
    }
    private void Update()
    {
        if (currentAmmo <= 0 && !reloading)
        {
            StartCoroutine(Refill());
            reloading = true;
        }
    }

    public void LoseAmmo(float amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo - amount, 0, maxAmmo);
    }
    private IEnumerator Refill()
    {
        if (currentReserve >= 0)
        {
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = Mathf.Clamp(currentReserve, 0, maxAmmo);
            currentReserve = Mathf.Clamp(currentReserve - maxAmmo, 0, Mathf.Infinity);
            reloading = false;
        }
        if (currentReserve <= 0 && currentAmmo <= 0)
        {
            outOfAmmo = true;
        }
        else
        {
            outOfAmmo = false;
        }
        
    }
}
