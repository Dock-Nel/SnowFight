using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class TutorialPrompt : MonoBehaviour
{
    float MouseSensitivity;
    public GameObject Player;
    public BotControllerNoGameManager Bot;
    public GameObject PanelTuto;
    private Animator animator;
    private bool isOpen = true;

    void Awake()
    {
        MouseSensitivity = Player.GetComponent<PlayerController>().rotationSpeed;
        if (PanelTuto != null)
        {
            animator = PanelTuto.GetComponent<Animator>();

            if (animator != null)
            {
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            }
        }
    }

    void Start()
    {
        if (animator != null)
        {
            animator.SetBool("Open", isOpen);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            Time.timeScale = 0f;
            Player.GetComponent<PlayerController>().rotationSpeed = 0f;
        }


        if (Input.GetKeyDown(KeyCode.Return) && Bot != null && !Bot.dead)
        {
            if (isOpen)
            {
                isOpen = false;

                if (animator != null)
                {
                    animator.SetBool("Open", false);
                }

                Time.timeScale = 1.0f;
                Player.GetComponent<PlayerController>().rotationSpeed = MouseSensitivity;
            }
        }
    }
}
