using UnityEngine;
using TMPro;

public class TestScoreManager : MonoBehaviour
{
    public static TestScoreManager Instance { get; private set; }

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text comboText;

    private int _score = 0;

    private int _combo = 0;

    private int scoreMultiplier = 1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Update()
    {
        ManageMultiplier();
    }

    public void AddPoint(int amount = 1)
    {
        _score += amount * scoreMultiplier;
        if (amount < 1) {
            ResetCombo();
        }
        else _combo += 1;
        comboText.text = _combo.ToString();
        scoreText.text = _score.ToString();
        Debug.Log($"Score updated: {_score}");
    }

    public void ResetScore()
    {
        _score = 0;
        scoreText.text = "0";
    }

    public void ResetCombo()
    {
        _combo = 0;
        comboText.text = "0";
    }


    public void ManageMultiplier() {
        if (_combo < 15) scoreMultiplier = 1;
        else if (_combo >= 15 && _combo < 30) scoreMultiplier = 2;
        else if (_combo >= 30 && _combo < 45) scoreMultiplier = 3;
        else if (_combo >= 45 && _combo < 60) scoreMultiplier = 4;
        else if (_combo >= 60) scoreMultiplier = 5;    
    }
}
