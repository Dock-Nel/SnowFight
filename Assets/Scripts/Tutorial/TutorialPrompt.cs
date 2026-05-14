using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialPrompt : MonoBehaviour
{
    float MouseSensitivity;
    public GameObject Player;
    public BotControllerNoGameManager Bot;

    void Awake()
    {
        MouseSensitivity = Player.GetComponent<PlayerController>().rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0f;
        Player.GetComponent<PlayerController>().rotationSpeed = 0f;

        if (Input.GetKey("return") && !Bot.dead)
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
            Player.GetComponent<PlayerController>().rotationSpeed = MouseSensitivity;
        }
    }
}
