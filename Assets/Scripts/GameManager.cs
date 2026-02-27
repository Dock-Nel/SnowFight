using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text tmpRounds;
    [SerializeField]
    private int roundCount = 1;
    [SerializeField]
    private List<GameObject> Bots = new List<GameObject>();
    [SerializeField]
    private GameObject botPrefab;
    [SerializeField]
    private Transform spawnPoint;


    void Start()
    {
        SpawnBots();
    }

    void Update()
    {
        tmpRounds.SetText("Round " + roundCount.ToString());

        if (Bots.Count == 0)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                roundCount++;
                SpawnBots();
            }       
        }     
    }
    private void SpawnBots()
    {
       
        for (int i = 0; i < roundCount + 2; i ++)
        {
            GameObject newBot = Instantiate(botPrefab, spawnPoint.position, spawnPoint.rotation);
            Bots.Add(newBot);
           
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
