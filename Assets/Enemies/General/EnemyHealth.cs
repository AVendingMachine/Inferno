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
    private GameObject waveManager;
    public GameObject ammoBox;

    private void Awake()
    {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
    }
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
            currentHealth -= 3*Time.deltaTime;
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
            Die();
        }
    }

    public void Die()
    {
        int randomInt = Random.Range(0, 10);
        if (randomInt >= 5)
        {
            Instantiate(ammoBox, transform.position, Quaternion.identity);
        }
        Instantiate(ragDoll, center.position, transform.rotation);
        waveManager.GetComponent<WaveManager>().deadEnemies++;
        Debug.Log("i despise you");
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
       // GetComponent<Bugman>().StartStagger();
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
