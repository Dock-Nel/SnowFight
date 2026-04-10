using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class BotControllerNoGameManager : MonoBehaviour
{
    public float health = 30;

    [SerializeField] string Title;
    [SerializeField][TextArea(15, 20)] string TextTuto;
    [SerializeField] string Prompt;
    [SerializeField] GameObject PanelTuto;

    TextMeshProUGUI VisualTitle;
    TextMeshProUGUI VisualTextTuto;
    TextMeshProUGUI VisualPrompt;

    private void Start()
    {
        VisualTitle = PanelTuto.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        VisualTextTuto = PanelTuto.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        VisualPrompt = PanelTuto.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health is now at: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        VisualTitle.text = Title;
        VisualTextTuto.text = TextTuto;
        VisualPrompt.text = Prompt;
        PanelTuto.SetActive(true);
        Destroy(gameObject);    
    }
}