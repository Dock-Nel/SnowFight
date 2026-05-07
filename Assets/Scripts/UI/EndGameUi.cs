using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGameUI : MonoBehaviour
{
    [Header("Player and Cameras")]
    [SerializeField] PlayerController playerController;
    [SerializeField] private Camera camSecondary;
    [SerializeField] private Camera camMain;

    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject UIGame;
    [SerializeField] private GameObject UIGeneral;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text roundsDisplay;

    [Header("Game Data")]
    [SerializeField] private TMP_Text currentRoundsText;

    private int finalScore;

    void Update()
    {
        if (playerController.Health == 0 || Input.GetKey(KeyCode.K))
        {
            OpenEndGameScreen();
        }
    }

    public void OpenEndGameScreen()
    {
        if (gameOverPanel == null)
        {
            Debug.LogError("Le panneau GameOverPanel n'est pas assigné !");
            return;
        }

        string rawText = currentRoundsText != null ? currentRoundsText.text : "0";
        string digits = new string(rawText.Where(char.IsDigit).ToArray());

        if (!int.TryParse(digits, out finalScore))
        {
            finalScore = 0;
        }

        gameOverPanel.SetActive(true);
        camMain.gameObject.SetActive(false);
        camSecondary.gameObject.SetActive(true);
        UIGame.SetActive(false);
        UIGeneral.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerController.rotationSpeed = 0f;

        Time.timeScale = 0f;

        if (roundsDisplay != null)
        {
            roundsDisplay.text = ("You reached round " + finalScore + " !" );
        }
    }

    public void OnClickSubmit()
    {
        string playerName = nameInputField.text;

        if (LeaderboardManager.instance != null)
        {
            LeaderboardManager.instance.AddScore(playerName, finalScore);
            Debug.Log("Score soumis : " + playerName + " - Round : " + finalScore);
        }
        else
        {
            Debug.LogError("LeaderboardManager introuvable dans la scène !");
        }
        Debug.Log("clic");
        SceneManager.LoadScene("Main Menu");
    }
}