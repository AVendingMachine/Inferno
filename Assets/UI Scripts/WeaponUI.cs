using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public TMP_Text ammoCounter;
    public TMP_Text itemNameDisplay;
    public GameObject Player;
    private GameObject selectedItem;

    private void Update()
    {
        selectedItem = Player.GetComponent<PlayerInventory>().currentItem;
        itemNameDisplay.text = selectedItem.name;
    }
}
