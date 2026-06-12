using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI;

public class SettingsValues : MonoBehaviour
{
    [Header("Graphics")]
    [Header("VSync")]
    [SerializeField] Toggle VSyncToggle;
    [Header("FPSCap")]
    [SerializeField] Toggle FPSCapToggle;
    [SerializeField] Slider FPSCapSlider;

    [Header("Audio")]
    [SerializeField] AudioMixer Mixer;
    [Header("MasterVolume")]
    [SerializeField] Slider MasterVolumeSlider;
    [Header("Music")]
    [SerializeField] Toggle MusicToggle;

    [Header("Accesibility")]
    [Header("Sensitivity")]
    [SerializeField] Slider SensitivitySlider;
    [Header("SnowEffect")]
    [SerializeField] Toggle SnowEffectToggle;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    //Graphics Settings
    public void VSync()
    {
        if (VSyncToggle.isOn)
        {
            Debug.Log("VSyncOn");
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            Debug.Log("VSyncOff");
            QualitySettings.vSyncCount = 0;
        }
    }

    public void FPSCap()
    {
        if (FPSCapToggle.isOn)
        {
            Debug.Log("FPSCapOn : " + FPSCapSlider.value);
            Application.targetFrameRate = (int)FPSCapSlider.value;
        }
        else
        {
            Debug.Log("FPSCapOff");
            Application.targetFrameRate = 0;
        }
    }

    //Audio Settings
    public void MasterVolume()
    {
        int VolumeValue = (((int)MasterVolumeSlider.value * 80) / 100) - 80; //Converts the percentage to a range between -80 to 0 (to be put as a dB value)
        Debug.Log(VolumeValue);
        Mixer.SetFloat("", VolumeValue);
    }
}
