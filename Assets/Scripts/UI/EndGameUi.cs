using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class EndGameUI : MonoBehaviour
{
    SceneSwitcher sceneSwitcher;

    [Header("Player and Cameras")]
    [SerializeField] PlayerController playerController;
    [SerializeField] private Camera camSecondary;
    [SerializeField] private Camera camMain;

    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private float fadeDuration = 0.6f;
    [SerializeField] private GameObject UIGame;
    [SerializeField] private GameObject UIGeneral;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text roundsDisplay;
    [SerializeField] private AudioManager audioManager;

    [Header("Game Data")]
    [SerializeField] private TMP_Text currentRoundsText;

    private int finalScore;
    private bool isScreenOn = false;

    void Update()
    {
        if (playerController.Health == 0 && !isScreenOn)
        {
            OpenEndGameScreen();
            isScreenOn = true;
            audioManager.StopMainMusic();
        }
    }

    public void OpenEndGameScreen()
    {
        if (gameOverPanel == null)
        {
            return;
        }

        string rawText = currentRoundsText != null ? currentRoundsText.text : "0";
        string digits = new string(rawText.Where(char.IsDigit).ToArray());

        if (!int.TryParse(digits, out finalScore))
        {
            finalScore = 0;
        }

        gameOverPanel.SetActive(true);

        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 0f;
            StartCoroutine(FadeInPanel());
        }

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
    private IEnumerator FadeInPanel()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            gameOverCanvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }
        gameOverCanvasGroup.alpha = 1f;
    }

    public void OnClickSubmit()
    {
        Time.timeScale = 0f;
        string playerName = nameInputField.text;

        if (LeaderboardManager.instance != null)
        {
            LeaderboardManager.instance.AddScore(playerName, finalScore);
        }
        sceneSwitcher = GetComponent<SceneSwitcher>();
        sceneSwitcher.SwitchToMainMenuScene();
    }
}