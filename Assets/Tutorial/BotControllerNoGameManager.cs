using UnityEngine;
using TMPro;
using System.Collections;


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
    [SerializeField] SpriteRenderer Sprite;

    private GameObject Player;
    private float MouseSensitivity;
    private Animator animator;

    TextMeshProUGUI VisualTitle;
    TextMeshProUGUI VisualTextTuto;
    TextMeshProUGUI VisualPrompt;

    private void Start()
    {
        VisualTitle = PanelTuto.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        VisualTextTuto = PanelTuto.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        VisualPrompt = PanelTuto.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            MouseSensitivity = Player.GetComponent<PlayerController>().rotationSpeed;
        }

        if (PanelTuto != null)
        {
            animator = PanelTuto.GetComponent<Animator>();
            if (animator != null)
            {
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            }
        }
    }

    private void Update()
    {
        if (dead)
        {
            Time.timeScale = 0f;
            if (Player != null)
            {
                Player.GetComponent<PlayerController>().rotationSpeed = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {

                if (animator != null)
                {
                    animator.SetBool("Open", false);
                }
                Time.timeScale = 1.0f;
                if (Player != null)
                {
                    Player.GetComponent<PlayerController>().rotationSpeed = MouseSensitivity;
                }

                SceneSwitch.SwitchToGameScene();
                Destroy(gameObject);
            }
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
        PanelTuto.SetActive(true);

        VisualTitle.text = Title;
        VisualTextTuto.text = TextTuto;
        VisualPrompt.text = Prompt;

        dead = true;

        if (animator != null)
        {
            animator.SetBool("Open", true);
        }
    }
}