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
    public float MasterVolumeValue;
    [Header("Music")]
    [SerializeField] Toggle MusicToggle;
    [SerializeField] AudioManager audioManager;
    public bool MusicBool;

    [Header("Accesibility")]
    [Header("Sensitivity")]
    [SerializeField] Slider SensitivitySlider;
    public float SensitivityValue;
    [Header("SnowEffect")]
    [SerializeField] Toggle SnowEffectToggle;
    public bool SnowEffectBool;

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

            //Graphics Settings
            if (instance.MotionBlurBool)
            {
                MotionBlurToggle.isOn = true;
            }
            else
            {
                MotionBlurToggle.isOn = false;
            }
            instance.MotionBlurToggle = MotionBlurToggle;
            instance.MotionBlurToggle.onValueChanged.RemoveAllListeners();
            instance.MotionBlurToggle.onValueChanged.AddListener(instance.MotionBlur);
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
            instance.VSyncToggle.onValueChanged.RemoveAllListeners();
            instance.VSyncToggle.onValueChanged.AddListener(instance.VSync);

            if (instance.FPSCapBool)
            {
                FPSCapToggle.isOn = true;
            }
            else
            {
                FPSCapToggle.isOn = false;
            }
            FPSCapSlider.value = FPSCapValue;
            instance.FPSCapToggle = FPSCapToggle;
            instance.FPSCapToggle.onValueChanged.RemoveAllListeners();
            instance.FPSCapToggle.onValueChanged.AddListener(instance.FPSCapWithBool);
            instance.FPSCapSlider = FPSCapSlider;
            instance.FPSCapSlider.value = instance.FPSCapValue;
            instance.FPSCapSlider.onValueChanged.RemoveAllListeners();
            instance.FPSCapSlider.onValueChanged.AddListener(instance.FPSCapWithFloat);

            //Audio Settings
            instance.Mixer = Mixer;

            MasterVolumeSlider.value = instance.MasterVolumeValue;
            instance.MasterVolumeSlider = MasterVolumeSlider;
            instance.MasterVolumeSlider.onValueChanged.RemoveAllListeners();
            instance.MasterVolumeSlider.onValueChanged.AddListener(instance.MasterVolume);

            if (instance.MusicBool)
            {
                MusicToggle.isOn = true;
            }
            else
            {
                MusicToggle.isOn = false;
            }
            instance.MusicToggle = MusicToggle;
            instance.MusicToggle.onValueChanged.RemoveAllListeners();
            instance.MusicToggle.onValueChanged.AddListener(instance.Music);
            instance.audioManager = audioManager;

            //Accessibility Settings
            SensitivitySlider.value = instance.SensitivityValue;
            instance.SensitivitySlider = SensitivitySlider;
            instance.SensitivitySlider.onValueChanged.RemoveAllListeners();
            instance.SensitivitySlider.onValueChanged.AddListener(instance.Sensitivity);

            if (instance.SnowEffectBool)
            {
                SnowEffectToggle.isOn = true;
            }
            else
            {
                SnowEffectToggle.isOn = false;
            }
            instance.SnowEffectToggle = SnowEffectToggle;
            instance.SnowEffectToggle.onValueChanged.RemoveAllListeners();
            instance.SnowEffectToggle.onValueChanged.AddListener(instance.SnowEffect);
            Destroy(gameObject);
            return;
        }
    }

    //Graphics Settings
    public void MotionBlur(bool a) //For some reason Adding a listener to a toggle via code requires a bool (I have not a single clue why), so its normal if the bool is never used
    {
        volume.TryGet<MotionBlur>(out Blur);
        if (MotionBlurToggle.isOn)
        {
            Blur.active = true;
            MotionBlurBool = true;
        }
        else
        {
            Blur.active = false;
            MotionBlurBool = false;
        }
    }

    public void VSync(bool a)
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

    public void FPSCapWithBool(bool a)
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

    public void FPSCapWithFloat(float a) //Its the exact same function as before but as the Toggle requires a bool for AddListener, a Slider requires a float, which in that specific case makes it quite ugly
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
    public void MasterVolume(float a)
    {
        MasterVolumeValue = MasterVolumeSlider.value;
        int VolumeValue = (((int)MasterVolumeValue * 80) / 100) - 80; //Converts the percentage to a range between -80 to 0 (to be put as a dB value)
        Mixer.SetFloat("MasterVolume", VolumeValue);
    }

    public void Music(bool a)
    {
        if (MusicToggle.isOn)
        {
            audioManager.StartMainMusic();
            MusicBool = true;
        }
        else
        {
            audioManager.StopMainMusic();
            MusicBool = false;
        }
    }

    //Accessibility Settings
    public void Sensitivity(float a)
    {
        SensitivityValue = SensitivitySlider.value; 
    }

    public void SnowEffect(bool a)
    {
        if (SnowEffectToggle.isOn)
        {
            SnowEffectBool = true;
        }
        else
        {
            SnowEffectBool = false;
        }
    }
}
