using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] string Title;
    [SerializeField][TextArea(15, 20)] string TextTuto;
    [SerializeField] GameObject PanelTuto;

    TextMeshProUGUI VisualTitle;
    TextMeshProUGUI VisualTextTuto;

    void Start()
    {
        VisualTitle = PanelTuto.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        VisualTextTuto = PanelTuto.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NoCollision")
        {
            PanelTuto.SetActive(true);
            VisualTitle.text = Title;
            VisualTextTuto.text = TextTuto;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NoCollision")
        {
            Destroy(gameObject);
        }
    }
}
