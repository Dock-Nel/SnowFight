using UnityEngine;
using UnityEngine.UI;

public class Vsync : MonoBehaviour
{
    [SerializeField] Toggle Toggle;
    public void OnValueChanged()
    {
        if (Toggle.isOn)
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
}
