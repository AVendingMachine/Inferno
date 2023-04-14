using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingControl : MonoBehaviour
{
    // Start is called before the first frame update

    public PostProcessVolume postVolume;
    public MainMenuScript mainMenuScript;
    private Bloom bloom;
    private Grain filmGrain;
    private AmbientOcclusion ao;
    private ColorGrading colorGrade;
    private Vignette vignette;
    private MotionBlur motionBlur;
    private DepthOfField depthOfField;
    private ScreenSpaceReflections ssr;
    void Start()
    {
        postVolume.profile.TryGetSettings(out bloom);
        postVolume.profile.TryGetSettings(out ao);
        postVolume.profile.TryGetSettings(out filmGrain);
        postVolume.profile.TryGetSettings(out colorGrade);
        postVolume.profile.TryGetSettings(out vignette);
        postVolume.profile.TryGetSettings(out motionBlur);
        postVolume.profile.TryGetSettings(out depthOfField);
        postVolume.profile.TryGetSettings(out ssr);

    }

    // Update is called once per frame
    void Update()
    {
        bloom.active = MainMenuScript.bloomToggle;
        ao.active = MainMenuScript.ambientOcclusion;
        filmGrain.active = MainMenuScript.grain;
        colorGrade.active = MainMenuScript.colorGrading;
        vignette.active = MainMenuScript.vignette;
        motionBlur.active = MainMenuScript.motionBlur;
        ssr.active = MainMenuScript.ssr;
        depthOfField.active = MainMenuScript.depthOField;
    }
}
