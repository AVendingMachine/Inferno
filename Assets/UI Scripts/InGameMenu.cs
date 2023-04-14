using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static bool gamePaused = true;
    bool inGameMenuEnabled = false;
    public GameObject inGameMenu;
    public PlayerCamera playerCam;

 
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void BloomToggle()
    {
        MainMenuScript.bloomToggle = !MainMenuScript.bloomToggle;
    }
    public void GrainToggle()
    {
        MainMenuScript.grain = !MainMenuScript.grain;
    }
    public void AOToggle()
    {
        MainMenuScript.ambientOcclusion = !MainMenuScript.ambientOcclusion;
    }
    public void ColorGradingToggle()
    {
        MainMenuScript.colorGrading = !MainMenuScript.colorGrading;
    }
    public void MotionBlurToggle()
    {
        MainMenuScript.motionBlur = !MainMenuScript.motionBlur;
    }
    public void VignetteToggle()
    {
        MainMenuScript.vignette = !MainMenuScript.vignette;
    }
    public void SSRToggle()
    {
        MainMenuScript.ssr = !MainMenuScript.ssr;
    }
    public void DepthOFieldToggle()
    {
        MainMenuScript.depthOField = !MainMenuScript.depthOField;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inGameMenuEnabled = !inGameMenuEnabled;
        }
        if (inGameMenuEnabled)
        {
            gamePaused = true;
            inGameMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            playerCam.enabled = false;
        }
        if (!inGameMenuEnabled)
        {
            gamePaused = false;
            inGameMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            playerCam.enabled = true;
        }

    }
}
