using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> items;
    private int selectedItem = 0;
    void Update()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) 
        {
            selectedItem = Mathf.Clamp(selectedItem + 1,-1, items.Count);
           // Debug.Log("forward");
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedItem = Mathf.Clamp(selectedItem - 1, -1, items.Count);
          //  Debug.Log("backward");
        }
        if (selectedItem > items.Count-1)
        {
            selectedItem = 0;
        }
        if (selectedItem < 0)
        {
            selectedItem = items.Count-1;
        }
        foreach (GameObject item in items)
        {
            if (items.IndexOf(item) == selectedItem)
            {
                item.SetActive(true);
            }
            if (items.IndexOf(item) != selectedItem)
            {
                item.SetActive(false);
            }

        }
    }
}

