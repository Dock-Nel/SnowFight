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
        DataPowerUp choice1 = GetRandomPowerUp();
        DataPowerUp choice2 = GetRandomPowerUp();


        
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

        Debug.Log("Choix 1 : " + choice1.powerUpName);
        Debug.Log("Choix 2 : " + choice2.powerUpName);

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


    //Cette partie est faite par l'IA, elle me sert à tester les tirages et la cohérence d'apparation des powerups

    [ContextMenu("Lancer Simulation (10 000 tirages)")]
    public void RunSimulation()
    {
        int totalTrials = 10000;

        // Dictionnaires pour compter les résultats
        Dictionary<DataPowerUp.PoolType, int> categoryCounts = new Dictionary<DataPowerUp.PoolType, int>();
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();

        // Initialisation
        categoryCounts[DataPowerUp.PoolType.Passif] = 0;
        categoryCounts[DataPowerUp.PoolType.Tool] = 0;
        categoryCounts[DataPowerUp.PoolType.Ephemere] = 0;

        for (int i = 0; i < totalTrials; i++)
        {
            // On simule un tirage complet
            DataPowerUp result = GetRandomPowerUp();

            // On compte la catégorie
            categoryCounts[result.category]++;

            // On compte l'objet précis
            if (!itemCounts.ContainsKey(result.powerUpName))
                itemCounts[result.powerUpName] = 0;
            itemCounts[result.powerUpName]++;
        }

        // --- AFFICHAGE DU RÉSUMÉ ---
        string report = $"<b>RÉSULTAT DE LA SIMULATION ({totalTrials} tirages)</b>\n\n";

        report += "<b>PAR CATÉGORIE (Rouge) :</b>\n";
        foreach (var cat in categoryCounts)
        {
            float pct = (cat.Value / (float)totalTrials) * 100f;
            report += $"- {cat.Key}: {pct:F1}% (Attendu: {(cat.Key == DataPowerUp.PoolType.Passif ? 35 : cat.Key == DataPowerUp.PoolType.Tool ? 25 : 40)}%)\n";
        }

        report += "\n<b>PAR OBJET (Vert) :</b>\n";
        foreach (var item in itemCounts)
        {
            float pct = (item.Value / (float)totalTrials) * 100f;
            report += $"- {item.Key}: {pct:F1}%\n";
        }

        Debug.Log(report);
    }


}