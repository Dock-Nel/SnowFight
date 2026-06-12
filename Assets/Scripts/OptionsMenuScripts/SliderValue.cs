using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    [SerializeField] Slider Slider;
    [SerializeField] TextMeshProUGUI Value;
    // Update is called once per frame
    void Update()
    {
        Value.text = Slider.value.ToString();
    }
}
