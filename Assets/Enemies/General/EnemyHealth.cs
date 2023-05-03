using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10;
    public GameObject ragDoll;
    public float maxBurnTime = 10f;
    public float currentHealth;
    private bool onFire = false;
    private float fireTime = 0f;
    public ParticleSystem fireSystem;
    public ParticleSystem deathSytem;
    public Transform center;
    private GameObject waveManager;
    public GameObject ammoBox;
    public bool ragDollOnDeath = true;
    bool dying = false;
    public GameObject model;

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
        
        if (currentHealth <= 0 && !dying)
        {
            Die();
        }
    }

    public void Die()
    {
        dying = true;
        int randomInt = Random.Range(0, 10);
        if (randomInt >= 5)
        {
            Instantiate(ammoBox, transform.position, Quaternion.identity);
        }
        if (ragDollOnDeath)
        {
            Instantiate(ragDoll, center.position, transform.rotation);
            Destroy(gameObject);
        }
        if (!ragDollOnDeath)
        {
            deathSytem.Play();
            model.GetComponent<SkinnedMeshRenderer>().enabled = false;
            StartCoroutine(DeathTimer());
        }
        waveManager.GetComponent<WaveManager>().deadEnemies++;
        Debug.Log("i despise you");
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
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
   

}
