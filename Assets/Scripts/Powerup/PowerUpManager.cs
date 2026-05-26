using UnityEngine;
using System.Collections.Generic;
public class PowerUpManager : MonoBehaviour
{
    [Header("Data Pools")]
    public List<DataPowerUp> passifPool;
    public List<DataPowerUp> toolPool;
    public List<DataPowerUp> ephemerePool;

    [Header("UI References")]
    public PowerUpButton buttonOne;
    public PowerUpButton buttonTwo;

    public void GenerateChoice()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        DataPowerUp equippedTool = player.GetCurrentTool();

        DataPowerUp choice1 = GetRandomPowerUp();

        //l'outil équipé = reroll
        while (equippedTool != null && choice1 == equippedTool)
        {
            Debug.Log("CHOICE 1 DEVAIT ÊTRE " + choice1 + "MAIS TU AS DEJA LE BONUS EN MAIN");
            choice1 = GetRandomPowerUp();
        }

        DataPowerUp choice2 = GetRandomPowerUp();

        while (equippedTool != null && choice2 == equippedTool)
        {
            Debug.Log("CHOICE 2 DEVAIT ÊTRE " + choice2 + "MAIS TU AS DEJA LE BONUS EN MAIN");
            choice2 = GetRandomPowerUp();
        }

        if (choice1.familyID == 6)
        {
            choice2 = choice1;
        }
        else if (choice2.familyID == 6)
        {
            choice1 = choice2;
        }
        else
        {
            while (choice1 == choice2)
            {
                choice2 = GetRandomPowerUp();
            }

            while (choice1.familyID == choice2.familyID)
            {
                choice2 = GetRandomPowerUp();
                choice1 = GetRandomPowerUp();
            }

        }

        //Debug.Log("Choix 1 : " + choice1.powerUpName);
        //Debug.Log("Choix 2 : " + choice2.powerUpName);

        // send to UI
        buttonOne.Setup(choice1);
        buttonTwo.Setup(choice2);
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

        foreach (var item in pool)
        {
            totalPercent += item.percentage;
        }

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
    public void NoMorePowerUps(DataPowerUp data)
    {
        if (data.IsMaxedOut())
        {

            if (passifPool.Contains(data)) passifPool.Remove(data);
            if (toolPool.Contains(data)) toolPool.Remove(data);
            if (ephemerePool.Contains(data)) ephemerePool.Remove(data);

            Debug.Log("Le powerup " + data.powerUpName + " a été définitivement retiré des tirages !");
        }
    }
}