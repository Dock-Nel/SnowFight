using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class DifficultRoundsManager : MonoBehaviour
{
    [SerializeField] GameObject UIDifficultRound;
    [SerializeField] TextMeshProUGUI DifficultRoundDescription;

    [Header("Data Pool")]
    public List<DataDifficultRound> Pool; 

    public void UISwitch(string Description)
    {
        if (UIDifficultRound.activeSelf)
        {
            UIDifficultRound.SetActive(false);
        }
        else
        {
            DifficultRoundDescription.text = Description;
            UIDifficultRound.SetActive(true);
        }
    }

    public DataDifficultRound GenerateChoice()
    {
        float totalPercent = 0;

        foreach (var item in Pool)
        {
            totalPercent += item.percentage;
        }

        float randomRoll = Random.Range(0, totalPercent);
        float currentWeight = 0;

        foreach (var item in Pool)
        {
            currentWeight += item.percentage;
            if (randomRoll <= currentWeight)
            {
                return item;
            }
        }
        return Pool[0];
    }
}
