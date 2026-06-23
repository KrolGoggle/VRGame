using UnityEngine;
using TMPro;

public class ArcadeNameInput : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text score;
    public TMP_Text[] letterTexts = new TMP_Text[3];

    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private int[] letterIndices = { 0, 0, 0 };

    void Start()
    {
        UpdateDisplay();
        score.text = ScoreManager.Instance?.GetLastScore().ToString();
    }

    public void NextLetter(int slotIndex)
    {
        letterIndices[slotIndex]++;

        if (letterIndices[slotIndex] >= alphabet.Length)
        {
            letterIndices[slotIndex] = 0;
        }
        UpdateDisplay();
    }

    public void PrevLetter(int slotIndex)
    {
        letterIndices[slotIndex]--;

        if (letterIndices[slotIndex] < 0)
        {
            letterIndices[slotIndex] = alphabet.Length - 1;
        }
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < 3; i++)
        {
            letterTexts[i].text = alphabet[letterIndices[i]].ToString();
        }
    }

    public void SubmitScore()
    {
        string finalName = alphabet[letterIndices[0]].ToString() +
                           alphabet[letterIndices[1]].ToString() +
                           alphabet[letterIndices[2]].ToString();

        int currentScore = ScoreManager.Instance.GetLastScore();

        LeaderboardManager.Instance.AddNewScoreAndSave(finalName, currentScore);
        Debug.Log("Zapisano wynik dla gracza: " + finalName);
    }
}