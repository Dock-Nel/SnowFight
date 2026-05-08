using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    private Slider slider;
    [SerializeField] GameObject Third;
    [SerializeField] GameObject Quarter;
    [SerializeField] GameObject Fifth;
    [SerializeField] GameObject Seventh;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (slider.maxValue == 30)
        {
            Third.SetActive(true);
            Quarter.SetActive(false);
            Fifth.SetActive(false);
            Seventh.SetActive(false);
        }
        else if (slider.maxValue == 40)
        {
            Third.SetActive(false);
            Quarter.SetActive(true);
            Fifth.SetActive(false);
            Seventh.SetActive(false);
        }
        else if (slider.maxValue == 50)
        {
            Third.SetActive(false);
            Quarter.SetActive(false);
            Fifth.SetActive(true);
            Seventh.SetActive(false);
        }
        else if (slider.maxValue == 70)
        {
            Third.SetActive(false);
            Quarter.SetActive(false);
            Fifth.SetActive(false);
            Seventh.SetActive(true);
        }
        else
        {
            Third.SetActive(false);
            Quarter.SetActive(false);
            Fifth.SetActive(false);
            Seventh.SetActive(false);
        }
    }
}
