using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGameUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text roundsDisplay;

    [Header("Game Data")]
    [SerializeField] private TMP_Text currentRoundsText;

    private int finalScore;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
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

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (roundsDisplay != null)
        {
            roundsDisplay.text = "Vous avez atteint le Round : " + finalScore;
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

        SceneManager.LoadScene("Main Menu");
    }
}