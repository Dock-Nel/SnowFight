using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SettingsValues : MonoBehaviour
{
    public static SettingsValues instance;

    [Header("Graphics")]
    [Header("MotionBlur")]
    [SerializeField] Toggle MotionBlurToggle;
    [SerializeField] VolumeProfile volume;
    MotionBlur Blur;
    public bool MotionBlurBool;
    [Header("VSync")]
    [SerializeField] Toggle VSyncToggle;
    public bool VSyncBool;
    [Header("FPSCap")]
    [SerializeField] Toggle FPSCapToggle;
    [SerializeField] Slider FPSCapSlider;
    public bool FPSCapBool;
    public float FPSCapValue;

    [Header("Audio")]
    [SerializeField] AudioMixer Mixer;
    [Header("MasterVolume")]
    [SerializeField] Slider MasterVolumeSlider;
    [Header("Music")]
    [SerializeField] Toggle MusicToggle;
    [SerializeField] AudioSource MusicSource;

    [Header("Accesibility")]
    [Header("Sensitivity")]
    [SerializeField] Slider SensitivitySlider;
    [Header("SnowEffect")]
    [SerializeField] Toggle SnowEffectToggle;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //This part makes it so nothing is reset once you go back to the main menu

            
            if (instance.MotionBlurBool)
            {
                MotionBlurToggle.isOn = true;
            }
            else
            {
                MotionBlurToggle.isOn = false;
            }
            instance.MotionBlurToggle = MotionBlurToggle;
            instance.volume = volume;
            instance.Blur = Blur;

            if (instance.VSyncBool)
            {
                VSyncToggle.isOn = true;
            }
            else
            {
                VSyncToggle.isOn = false;
            }
            instance.VSyncToggle = VSyncToggle;

            FPSCapSlider.value = FPSCapValue;
            instance.FPSCapToggle = FPSCapToggle;
            instance.FPSCapSlider = FPSCapSlider;
            if (instance.FPSCapBool)
            {
                instance.FPSCapToggle.isOn = true;
            }
            else
            {
                instance.FPSCapToggle.isOn = false;
            }
            instance.FPSCapSlider.value = instance.FPSCapValue;


            instance.Mixer = Mixer;
            instance.MasterVolumeSlider = MasterVolumeSlider;
            instance.MusicToggle = MusicToggle;
            instance.MusicSource = MusicSource;
            instance.SensitivitySlider = SensitivitySlider;
            instance.SnowEffectToggle = SnowEffectToggle;
            Destroy(gameObject);
            return;
        }
    }

    //Graphics Settings
    public void MotionBlur()
    {
        volume.TryGet<MotionBlur>(out Blur);
        if (MotionBlurToggle.isOn)
        {
            Blur.intensity.value = 1f;
            MotionBlurBool = true;
        }
        else
        {
            Blur.intensity.value = 0f;
            MotionBlurBool = false;
        }
    }

    public void VSync()
    {
        if (VSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            VSyncBool = true;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            VSyncBool = false;
        }
    }

    public void FPSCap()
    {
        if (FPSCapToggle.isOn)
        {
            FPSCapValue = FPSCapSlider.value;
            Application.targetFrameRate = (int)FPSCapValue;
            FPSCapBool = true;
        }
        else
        {
            Application.targetFrameRate = 0;
            FPSCapBool = false;
        }
    }

    //Audio Settings
    public void MasterVolume()
    {
        int VolumeValue = (((int)MasterVolumeSlider.value * 80) / 100) - 80; //Converts the percentage to a range between -80 to 0 (to be put as a dB value)
        Debug.Log(VolumeValue);
        Mixer.SetFloat("MasterVolume", VolumeValue);
    }

    public void Music()
    {
        if (MusicToggle.isOn)
        {
            MusicSource.Play();
        }
        else
        {
            MusicSource.Stop();
        }
    }
}
