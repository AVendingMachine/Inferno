using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider sliderBar;
    public GameObject player;
    private float maximumHealth;
    private void Awake()
    {
        maximumHealth = player.GetComponent<PlayerHealth>().maxHealth;
        sliderBar.maxValue = maximumHealth;
        sliderBar.minValue = 0;
    }
    private void Update()
    {
        sliderBar.value = player.GetComponent<PlayerHealth>().currentHealth;
    }
}
