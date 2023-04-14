using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    bool inGameMenuEnabled = false;

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inGameMenuEnabled = !inGameMenuEnabled;
        }
    }
}
