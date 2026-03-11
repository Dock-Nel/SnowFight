using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    [Header("Pools de Données")]
    public List<DataPowerUp> passifPool;
    public List<DataPowerUp> toolPool;     
    public List<DataPowerUp> ephemerePool; 
    public void GenerateChoice()
    {
        DataPowerUp choice1 = GetRandomPowerUp();
        DataPowerUp choice2 = GetRandomPowerUp();

        while (choice1 == choice2)
        {
            choice2 = GetRandomPowerUp();
        }

        Debug.Log("Choix 1 : " + choice1.powerUpName);
        Debug.Log("Choix 2 : " + choice2.powerUpName);

        // send to UI
    }

    private DataPowerUp GetRandomPowerUp()
    {
        // Pools pick
        float roll = Random.Range(0f, 100f);
        List<DataPowerUp> selectedPool;

        if (roll <= 35f)
            selectedPool = passifPool;
        else if (roll <= 60f) //?
            selectedPool = toolPool;
        else
            selectedPool = ephemerePool;

        return GetItem(selectedPool);
    }

    private DataPowerUp GetItem(List<DataPowerUp> pool)
    {
        float totalPercent = 0;
        foreach (var item in pool) totalPercent += item.percentage;

        float randomRoll = Random.Range(0, totalPercent);
        float currentWeight = 0;

        foreach (var item in pool)
        {
            currentWeight += item.percentage;
            if (randomRoll <= currentWeight)
            {
                return item;
            }
        }
        return pool[0];
    }
}