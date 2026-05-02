using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    //Powerup

    public PowerUpManager managePowerup;
    private bool choicePowerup = false;

    //Buttons

    [SerializeField]
    private Button upgrade1;
    [SerializeField]
    private Button upgrade2;

    [SerializeField]
    private GameObject UIGame;
    [SerializeField]
    private GameObject UIItemPool;

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
    private List<GameObject> Bots = new List<GameObject>();
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
    }

    void Update()
    {
        tmpRounds.SetText("Round " + roundCount.ToString());
        if (Bots.Count == 0) 
        {
            if (choicePowerup == false)
            {
                managePowerup.GenerateChoice();
                choicePowerup = true;
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

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }

    void OnUpgradeSelected()
    {
        ButtonDisable();
        camSecondary.gameObject.SetActive(false);
        camMain.gameObject.SetActive(true);
        playerScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        roundCount++;
        choicePowerup = false;
        SpawnBots();
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
            GameObject newBot = Instantiate(botPrefab, spawnPoint[0].position, spawnPoint[0].rotation);
            Bots.Add(newBot);
            newBot.SetActive(true);
            RoutineSec();
        }
    }

    private void SpawnBots()
    {
        int Random = UnityEngine.Random.Range(0, spawnPoint.Count);
        Vector3 RandomPosition = new Vector3(UnityEngine.Random.Range(-5, 6), 0, UnityEngine.Random.Range(-5, 6));
        GameObject newBot = Instantiate(botPrefab, spawnPoint[Random].position, spawnPoint[Random].rotation);
        newBot.SetActive(true);
        Bots.Add(newBot);
        Debug.Log(Random);

        switch (roundCount)
        {
             case <= 3:
                 for (int i = 0; i < roundCount + 2; i++)
                 {
                    Random = UnityEngine.Random.Range(0, spawnPoint.Count);
                    RandomPosition = new Vector3(UnityEngine.Random.Range(-5, 6), 0, UnityEngine.Random.Range(-5, 6));
                    newBot = Instantiate(botPrefab, spawnPoint[Random].position + RandomPosition, spawnPoint[Random].rotation);
                    newBot.SetActive(true);
                    Bots.Add(newBot);
                }
                 break;
             case > 3 and <= 6 :
                 for (int i = 0; i < roundCount + 4; i++)
                 {
                    Random = UnityEngine.Random.Range(0, spawnPoint.Count);
                    RandomPosition = new Vector3(UnityEngine.Random.Range(-5, 6), 0, UnityEngine.Random.Range(-5, 6));
                    newBot = Instantiate(botPrefab, spawnPoint[Random].position + RandomPosition, spawnPoint[Random].rotation);
                    newBot.SetActive(true);
                    Bots.Add(newBot);
                }
                 break;
             case > 6 and <= 10:
                 for (int i = 0; i < roundCount + 8; i++)
                 {
                    Random = UnityEngine.Random.Range(0, spawnPoint.Count);
                    RandomPosition = new Vector3(UnityEngine.Random.Range(-5, 6), 0, UnityEngine.Random.Range(-5, 6));
                    newBot = Instantiate(botPrefab, spawnPoint[Random].position + RandomPosition, spawnPoint[Random].rotation);
                    newBot.SetActive(true);
                    Bots.Add(newBot);
                }
                 break;
             case > 10:
                 //faire scale le multiplicateur au fur et a mesure. Car passe de 18 ennemies vague 10 à 31 vague 11.
                 for (int i = 0; i < roundCount * 1.5 + 15; i++)
                 {
                    Random = UnityEngine.Random.Range(0, spawnPoint.Count);
                    RandomPosition = new Vector3(UnityEngine.Random.Range(-5, 6), 0, UnityEngine.Random.Range(-5, 6));
                    newBot = Instantiate(botPrefab, spawnPoint[Random].position + RandomPosition, spawnPoint[Random].rotation);
                    newBot.SetActive(true);
                    Bots.Add(newBot);
                }
                 break;
             
        }
        

    }
    public void RemoveBotFromList(GameObject botToRemove)
    {
        if (Bots.Contains(botToRemove))
        {
            Bots.Remove(botToRemove);
        }
    }

}
