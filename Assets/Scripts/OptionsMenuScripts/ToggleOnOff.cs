using UnityEngine;
using UnityEngine.UI;

public class ToggleOnOff : MonoBehaviour
{
    [SerializeField] Toggle Toggle;
    [SerializeField] GameObject Object;
    void Update()
    {
        if (Toggle.isOn)
        {
            Object.SetActive(true);
        }
        else
        {
            Object.SetActive(false);
        }
    }
}
