using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchToTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
