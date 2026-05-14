using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public bool MainMenu;
    public void Awake()
    {
        if (MainMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void SwitchToTutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void SwitchToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LeaveGame()
    {
        Application.Quit();
    }
}
