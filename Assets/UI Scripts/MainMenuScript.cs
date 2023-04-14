using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenu;
    public GameObject options1;
    public GameObject changeLog;

    public static bool bloomToggle = true;
    public static bool grain = true;
    public static bool ambientOcclusion = true;
    public static bool colorGrading = true;
    public static bool motionBlur = true;
    public static bool vignette = true;
    public static bool ssr = true;
    public static bool depthOField = true;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void SwitchToOptions1()
    {
        mainMenu.SetActive(false);
        options1.SetActive(true);
    }
    public void SwitchToMainMenu()
    {
        changeLog.SetActive(false);
        options1.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void SwitchToChangeLog()
    {
        mainMenu.SetActive(false);
        changeLog.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void BloomToggle()
    {
        bloomToggle = !bloomToggle;
    }
    public void GrainToggle()
    {
        grain = !grain;
    }
    public void AOToggle()
    {
        ambientOcclusion = !ambientOcclusion;
    }
    public void ColorGradingToggle()
    {
        colorGrading = !colorGrading;
    }
    public void MotionBlurToggle()
    {
        motionBlur = !motionBlur;
    }
    public void VignetteToggle()
    {
        vignette = !vignette;
    }
    public void SSRToggle()
    {
        ssr = !ssr;
    }
    public void DepthOFieldToggle()
    {
        depthOField = !depthOField;
    }



}
