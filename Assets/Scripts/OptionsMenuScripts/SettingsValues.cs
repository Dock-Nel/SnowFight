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
    [SerializeField] AudioSource MusicSource;
    public bool MusicBool;

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

            MasterVolumeSlider.value = MasterVolumeValue;
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
            instance.MusicSource = MusicSource;

            //Accessibility Settings 
            instance.SensitivitySlider = SensitivitySlider;
            instance.SnowEffectToggle = SnowEffectToggle;
            Destroy(gameObject);
            return;
        }
    }

    //Graphics Settings
    public void MotionBlur(bool a) //For some reason Adding a listener to a toggle via code requires a bool (I have not a single clue why), so its normal if the bool is never used
    {
        volume.TryGet<MotionBlur>(out Blur);
        if (Blur == null)
        {
            Debug.Log("BlurEmpty");
        }
        if (MotionBlurToggle.isOn)
        {
            Blur.intensity.value = 0.25f;
            MotionBlurBool = true;
        }
        else
        {
            Blur.intensity.value = 0f;
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
        Debug.Log(VolumeValue);
        Mixer.SetFloat("MasterVolume", VolumeValue);
    }

    public void Music(bool a)
    {
        if (MusicToggle.isOn)
        {
            MusicSource.Play();
            MusicBool = true;
        }
        else
        {
            MusicSource.Stop();
            MusicBool = false;
        }
    }
}
