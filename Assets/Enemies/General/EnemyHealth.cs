using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10;
    public GameObject ragDoll;
    public float maxBurnTime = 10f;
    private float currentHealth;
    private bool onFire = false;
    private float fireTime = 0f;
    public ParticleSystem fireSystem;
    public Transform center;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (onFire)
        {
            if (!fireSystem.isPlaying)
            {
                fireSystem.Play();
            }
            currentHealth -= Time.deltaTime;
        }
        if (!onFire)
        {
            if (fireSystem.isPlaying)
            {
                fireSystem.Stop();
            }
            
        }

        if (fireTime >0)
        {
            fireTime = Mathf.Clamp(fireTime - Time.deltaTime, 0, maxBurnTime);
            onFire = true;
        }
        if (fireTime == 0)
        {
            onFire = false;
        }
        
        if (currentHealth <= 0 )
        {
            Instantiate(ragDoll, center.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    public void CatchFire(float burnAmount)
    {
        Debug.Log("I caught fire");
        if (fireTime <= maxBurnTime)
        {
            fireTime += burnAmount;
        }
        
    }
   

}
