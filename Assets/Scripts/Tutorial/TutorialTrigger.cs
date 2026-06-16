using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] string Title;
    [SerializeField][TextArea(15, 20)] string TextTuto;
    [SerializeField] GameObject PanelTuto;

    private GameObject Player;
    private float MouseSensitivity;
    private Animator animator;
    private bool isTutoActive = false;

    TextMeshProUGUI VisualTitle;
    TextMeshProUGUI VisualTextTuto;

    void Start()
    {
        VisualTitle = PanelTuto.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        VisualTextTuto = PanelTuto.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

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
        if (isTutoActive)
        {
            Time.timeScale = 0f;
            if (Player != null)
            {
                Player.GetComponent<PlayerController>().rotationSpeed = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                isTutoActive = false;

                if (animator != null)
                {
                    animator.SetBool("Open", false); // Ferme l'animation
                }

                Time.timeScale = 1.0f;
                if (Player != null)
                {
                    Player.GetComponent<PlayerController>().rotationSpeed = MouseSensitivity;
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PanelTuto.SetActive(true);
            VisualTitle.text = Title;
            VisualTextTuto.text = TextTuto;
            isTutoActive = true;

            if (animator != null)
            {
                animator.SetBool("Open", true); // Ouvre l'animation
            }
        }
    }

}
