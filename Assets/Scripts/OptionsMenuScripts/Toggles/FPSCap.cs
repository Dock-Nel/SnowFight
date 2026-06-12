using UnityEngine;
using UnityEngine.UI;

public class FPSCap : MonoBehaviour
{
    [SerializeField] Toggle Toggle;
    [SerializeField] Slider Value;
    public void OnValueChanged()
    {
        if (Toggle.isOn)
        {
            Debug.Log("FPSCapOn : " + Value.value);
            Application.targetFrameRate = (int)Value.value;
        }
        else
        {
            Debug.Log("FPSCapOff");
            QualitySettings.vSyncCount = 0;
        }
    }
}
