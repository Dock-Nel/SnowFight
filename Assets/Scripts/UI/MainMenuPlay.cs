using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameManager";

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
        Debug.Log("Chargement de la scène : " + gameSceneName);
    }
}