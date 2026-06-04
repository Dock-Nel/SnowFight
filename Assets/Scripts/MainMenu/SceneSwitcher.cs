using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public bool MainMenu;
    public Animator Transition;

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
        ChangeScene("Tutorial");
    }
    public void SwitchToGameScene()
    {
        ChangeScene("MainGame");
    }
    public void SwitchToMainMenuScene()
    {
        ChangeScene("MainMenu");
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneCoroutine(sceneName));
    }

    IEnumerator ChangeSceneCoroutine(string sceneName)
    {
        Time.timeScale = 1f;
        Debug.Log("Loading New Scene");
        Transition.SetBool("SceneEnd", true);
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("New Scene Loaded");
        Resources.Load(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
