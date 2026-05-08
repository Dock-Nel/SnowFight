using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int roundsReached;

    public PlayerScore(string name, int rounds)
    {
        playerName = name;
        roundsReached = rounds;
    }
}

[System.Serializable]
public class LeaderboardData
{
    public List<PlayerScore> scores = new List<PlayerScore>();
}

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;

    private string filePath;
    public LeaderboardData leaderboardData = new LeaderboardData();

    [Header("Configuration UI")]
    [SerializeField] private TMPro.TMP_Text displayArea;
    [SerializeField] private GameObject leaderboardBackground;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        filePath = Application.persistentDataPath + "/leaderboard.json";
        LoadScores();
    }

    public void AddScore(string name, int rounds)
    {
        if (string.IsNullOrEmpty(name)) name = "Anonyme";

        leaderboardData.scores.Add(new PlayerScore(name, rounds));

        // trier et top 10
        leaderboardData.scores = leaderboardData.scores
            .OrderByDescending(s => s.roundsReached)
            .Take(10)
            .ToList();

        SaveScores();
        UpdateDisplay();
    }

    [ContextMenu("Save Scores")]
    private void SaveScores()
    {
        string json = JsonUtility.ToJson(leaderboardData, true);
        File.WriteAllText(filePath, json);
    }

    [ContextMenu("Load Scores")]
    private void LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            leaderboardData = JsonUtility.FromJson<LeaderboardData>(json);
        }
    }

    public void UpdateDisplay()
    {
        if (displayArea == null) return;

        displayArea.text = "<b>TOP 10 BEST THROWERS</b>\n\n";

        for (int i = 0; i < leaderboardData.scores.Count; i++)
        {
            var entry = leaderboardData.scores[i];
            displayArea.text += $"{i + 1}. {entry.playerName} - Round {entry.roundsReached}\n";
        }
    }

    public void ToggleLeaderboard()
    {
        if (leaderboardBackground != null)
        {
            bool isCurrentlyActive = leaderboardBackground.activeSelf;
            leaderboardBackground.SetActive(!isCurrentlyActive);
        }
    }

    [ContextMenu("Ouvrir le dossier de sauvegarde")]
    public void OpenSaveFolder()
    {
        System.Diagnostics.Process.Start("explorer.exe", Application.persistentDataPath.Replace("/", "\\"));
    }
}