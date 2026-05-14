using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public DifficultRoundsManager manageDifficultRounds;
    private bool DifficultRoundSetup = false;
    DataDifficultRound currentData;

    //Powerup

    public PowerUpManager managePowerup;
    private bool choicePowerup = false;

    //Buttons

    [SerializeField]
    private Button upgrade1;
    [SerializeField]
    private Button upgrade2;


    [Header("UI")]
    [SerializeField]
    private GameObject UIGame;
    [SerializeField]
    private GameObject UIItemPool;
    [SerializeField] 
    private GameObject UIGeneral;
    [SerializeField] 
    private GameObject UIDifficultRound;
    [SerializeField]
    private GameObject UIGameOver;
    [SerializeField] 
    private GameObject UIPause;
    [SerializeField]
    private TextMeshProUGUI UIGingerbreadCountdown;
    [SerializeField] 
    bool WasInGame = false;
    [SerializeField] 
    bool WasChoosingItems = false;
    [SerializeField] 
    bool WasOnDifficultWarning = false;

    //Cam

    [SerializeField]
    private Camera camSecondary;
    [SerializeField]
    private Camera camMain;
    [SerializeField]
    private PlayerController playerScript;

    //GameManager

    [SerializeField]
    private TMP_Text tmpRounds;
    [SerializeField]
    private int roundCount = 1;
    [SerializeField]
    public List<GameObject> Bots = new List<GameObject>();
    [SerializeField]
    private GameObject botPrefab;
    [SerializeField]
    private List<Transform> spawnPoint = new List<Transform>();

    void Start()
    {
        SpawnBots();
        upgrade1.onClick.AddListener(OnUpgradeSelected);
        upgrade2.onClick.AddListener(OnUpgradeSelected);
        camMain.gameObject.SetActive(true);
        camSecondary.gameObject.SetActive(false);
        Time.timeScale = 1.0f; 
    }

    void Update()
    {
        tmpRounds.SetText("Round " + roundCount.ToString());
        UIGingerbreadCountdown.SetText("{0} Gingerbread left", Bots.Count);
        if (Bots.Count == 0 && !DifficultRoundSetup && !UIPause.activeSelf) 
        {
            if (choicePowerup == false)
            {
                managePowerup.GenerateChoice();
                choicePowerup = true;
            }
            if (currentData != null)
            {
                currentData.RevertEffect(playerScript, this);
                currentData = null;
            }

            StartCoroutine(Wait());
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerScript.enabled = false;
            camMain.gameObject.SetActive(false);
            camSecondary.gameObject.SetActive(true);
            Time.timeScale = 0;
            ButtonEnable();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UIPause.activeSelf && !UIGameOver.activeSelf)
            {
                Debug.Log("Pause");

                if (UIGame.activeSelf)
                {
                    WasInGame = true;
                }
                else if (UIItemPool.activeSelf)
                {
                    WasChoosingItems = true;
                }
                else if (UIDifficultRound.activeSelf)
                {
                    WasOnDifficultWarning = true;
                }
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                playerScript.enabled = false;

                camMain.gameObject.SetActive(false);
                camSecondary.gameObject.SetActive(true);

                UIGeneral.SetActive(false);

                UIGame.SetActive(false);
                UIItemPool.SetActive(false);
                UIDifficultRound.SetActive(false);

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
        if (WasInGame)
        {
            Debug.Log("Play");

            Time.timeScale = 1f;
            playerScript.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            camMain.gameObject.SetActive(true);
            camSecondary.gameObject.SetActive(false);

            UIGeneral.SetActive(true);
            UIGame.SetActive(true);

            UIPause.SetActive(false);

            WasInGame = false;
        }
        else if (WasChoosingItems)
        {
            Debug.Log("Play");
            
            UIGeneral.SetActive(true);
            UIItemPool.SetActive(true);

            UIPause.SetActive(false);

            WasChoosingItems = false;
        }
        else if (WasOnDifficultWarning)
        {
            Debug.Log("Play");

            UIGeneral.SetActive(true);
            UIDifficultRound.SetActive(true);

            UIPause.SetActive(false);

            WasOnDifficultWarning = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }

    void OnUpgradeSelected()
    {
        roundCount++;
        if (roundCount % 3 == 0)
        {
            SpawnBots();
            UIItemPool.SetActive(false);
            currentData = manageDifficultRounds.GenerateChoice();
            manageDifficultRounds.UISwitch(currentData.DifficultRoundDescription);
            currentData.ApplyEffect(playerScript, this);
            DifficultRoundSetup = true;
            Debug.Log("Difficult Round");
            
        }
        else
        {
            ButtonDisable();
            camSecondary.gameObject.SetActive(false);
            camMain.gameObject.SetActive(true);
            playerScript.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            choicePowerup = false;
            SpawnBots();
        }
    }

    public void OnDifficultRoundAknowledged()
    {
        DifficultRoundSetup = false;
        ButtonDisable();
        camSecondary.gameObject.SetActive(false);
        camMain.gameObject.SetActive(true);
        playerScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        choicePowerup = false;
    }

    void ButtonEnable()
    {
        UIGame.SetActive(false);
        UIItemPool.SetActive(true);
    }
    void ButtonDisable()
    {
        UIGame.SetActive(true);
        UIItemPool.SetActive(false);
    }
    IEnumerator RoutineSec()
    {
        yield return new WaitForSeconds(1f);
    }

    private void SpawnBotsLOL()
    {
        for (int i = 0; i < 600; i++)
        {
            SpawnSingleBot();
            RoutineSec();
        }
    }

    public void SpawnBots()
    {
        SpawnSingleBot();

        switch (roundCount)
        {
             case <= 3:
                 for (int i = 0; i < roundCount + 2; i++)
                 {
                    SpawnSingleBot();
                }
                 break;
             case > 3 and <= 6 :
                 for (int i = 0; i < roundCount + 4; i++)
                 {
                    SpawnSingleBot();
                }
                 break;
             case > 6 and <= 10:
                 for (int i = 0; i < roundCount + 8; i++)
                 {
                    SpawnSingleBot();
                }
                 break;
             case > 10:
                 //faire scale le multiplicateur au fur et a mesure. Car passe de 18 ennemies vague 10 à 31 vague 11.
                 for (int i = 0; i < roundCount * 1.5 + 15; i++)
                 {
                    SpawnSingleBot();
                 }
                 break;
             
        }  
    }

    public void SpawnSingleBot()
    {
        int Random = UnityEngine.Random.Range(0, spawnPoint.Count);
        Vector3 RandomPosition = new Vector3(UnityEngine.Random.Range(-5, 6), 0, UnityEngine.Random.Range(-5, 6));
        GameObject newBot = Instantiate(botPrefab, spawnPoint[Random].position, spawnPoint[Random].rotation);
        newBot.SetActive(true);
        Bots.Add(newBot);
    }

    public void RemoveBotFromList(GameObject botToRemove)
    {
        if (Bots.Contains(botToRemove))
        {
            Bots.Remove(botToRemove);
        }
    }

}
