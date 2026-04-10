using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class BotControllerNoGameManager : MonoBehaviour
{
    public float health = 30;
    public bool dead = false;

    [SerializeField] string Title;
    [SerializeField][TextArea(15, 20)] string TextTuto;
    [SerializeField] string Prompt;
    [SerializeField] GameObject PanelTuto;
    [SerializeField] SceneSwitcher SceneSwitch;
    [SerializeField] GameObject Gingerbread;

    TextMeshProUGUI VisualTitle;
    TextMeshProUGUI VisualTextTuto;
    TextMeshProUGUI VisualPrompt;

    private void Start()
    {
        VisualTitle = PanelTuto.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        VisualTextTuto = PanelTuto.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        VisualPrompt = PanelTuto.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKey("return") && dead == true)
        {
            SceneSwitch.SwitchToGameScene();
            Destroy(gameObject);
        }
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
        Gingerbread.SetActive(false);
        VisualTitle.text = Title;
        VisualTextTuto.text = TextTuto;
        VisualPrompt.text = Prompt;
        dead = true;
        PanelTuto.SetActive(true);
    }
}