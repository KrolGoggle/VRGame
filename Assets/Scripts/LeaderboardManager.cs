using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class PlayerSaveData
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class LeaderboardData
{
    public List<PlayerSaveData> highScores = new List<PlayerSaveData>();
}

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    [Header("UI References")]
    public TMP_Text displayBoardText;
    private int maxScoresOnBoard = 5;

    private string savePath = "./leaderboard.json";
    //private string savePath = Application.persistentDataPath + "/last_score.json";

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        LoadAndDisplayScores();
    }

    public void AddNewScoreAndSave(string nameToSave, int finalScore)
    {
        LeaderboardData currentBoard = LoadDataFromFile();

        PlayerSaveData newScore = new PlayerSaveData { playerName = nameToSave, score = finalScore };
        currentBoard.highScores.Add(newScore);

        currentBoard.highScores.Sort((a, b) => b.score.CompareTo(a.score));

        if (currentBoard.highScores.Count > maxScoresOnBoard)
        {
            currentBoard.highScores.RemoveRange(maxScoresOnBoard, currentBoard.highScores.Count - maxScoresOnBoard);
        }

        string json = JsonUtility.ToJson(currentBoard, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Zapisano now¹ tablicê wyników!");

        LoadAndDisplayScores();
    }

    private LeaderboardData LoadDataFromFile()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<LeaderboardData>(json);
        }
        else
        {
            return new LeaderboardData();
        }
    }

    public void LoadAndDisplayScores()
    {
        if (displayBoardText == null) return;

        LeaderboardData boardData = LoadDataFromFile();

        if (boardData.highScores.Count == 0)
        {
            displayBoardText.text = "NO HIGH SCORES YET";
            return;
        }

        string uiText = "";

        for (int i = 0; i < boardData.highScores.Count; i++)
        {
            string pName = boardData.highScores[i].playerName;
            int pScore = boardData.highScores[i].score;

            uiText += $"{i + 1}. {pName} ..... {pScore:D4}\n";
        }

        displayBoardText.text = uiText;
    }
}