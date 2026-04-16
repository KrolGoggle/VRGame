using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    public TMP_Text scoreText;

    private int _score;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddPoint(int amount = 1)
    {
        _score += amount;
        scoreText.text = _score.ToString();
    }

    public void ResetScore()
    {
        _score = 0;
        scoreText.text = "0";
    }
}
