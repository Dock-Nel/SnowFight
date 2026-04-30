using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    [Header("UI Rows")]
    [SerializeField] private List<GameObject> recordRows;

    void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (LeaderboardManager.instance == null)
        {
            Debug.LogWarning("LeaderboardManager introuvable !");
            return;
        }

        var scores = LeaderboardManager.instance.leaderboardData.scores;
        Debug.Log("Affichage des scores, nombre d'entrées : " + scores.Count);

        for (int i = 0; i < recordRows.Count; i++)
        {
            if (i < scores.Count)
            {
                recordRows[i].SetActive(true);
                var score = scores[i];

                TMP_Text[] texts = recordRows[i].GetComponentsInChildren<TMP_Text>(true);

                TMP_Text nameText = null;
                TMP_Text wavesText = null;

                foreach (var t in texts)
                {
                    if (t.gameObject.name.ToLower().Contains("name"))
                    {
                        nameText = t;
                    }
                    else if (t.gameObject.name.ToLower().Contains("wave") || t.gameObject.name.ToLower().Contains("round"))
                    {
                        wavesText = t;
                    }
                }

                if (nameText != null)
                {
                    nameText.text = score.playerName;
                }

                if (wavesText != null)
                {
                    wavesText.text = score.roundsReached.ToString();
                }
            }
            else
            {
                recordRows[i].SetActive(false);
            }
        }
    }
}