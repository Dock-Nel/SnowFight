using UnityEngine;

public class Pause : MonoBehaviour
{
    private AudioSource _ClickUIPause;
    public GameObject UIPause;
    public GameObject UIGeneral;
    public PlayerController playerScript;
    public GameObject camMain;
    public GameObject camSecondary;
    public GameObject PanelTuto;
    public AudioManager audioManager;
    void Start()
    {
        _ClickUIPause = GameObject.Find("ClickUIPause").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UIPause.activeSelf && Time.timeScale != 0f)
            {
                Debug.Log("Pause");

                audioManager.PauseMainMusic();
                PanelTuto.SetActive(false);

                _ClickUIPause.Play();

                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                playerScript.enabled = false;

                camMain.gameObject.SetActive(false);
                camSecondary.gameObject.SetActive(true);

                UIGeneral.SetActive(false);

                UIPause.SetActive(true);
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Play");
        audioManager.StartMainMusic();
        Time.timeScale = 1f;

        playerScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        camSecondary.gameObject.SetActive(false);
        camMain.gameObject.SetActive(true);

        UIGeneral.SetActive(true);

        UIPause.SetActive(false);
    }
}
