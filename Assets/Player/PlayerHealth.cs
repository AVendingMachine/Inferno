using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 30;
    public GameObject mainCamera;
    public float currentHealth;
    public PostProcessVolume postVolume;
    private ColorGrading colorGrade;
    public GameObject UIX;
    // Start is called before the first frame update
    void Start()
    {
        postVolume.profile.TryGetSettings(out colorGrade);
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
    }


    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        colorGrade.saturation.value = -100;
        mainCamera.AddComponent<Rigidbody>();
        mainCamera.AddComponent<BoxCollider>();
        UIX.SetActive(false);
        Debug.Log("you dead lol");
    }
}
