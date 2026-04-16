using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance { get; private set; }

    [Header("UI")]
    public TMP_Text livesText;

    [Header("Ustawienia")]
    public int maxLives = 3;

    private int _lives;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        _lives = maxLives;
        UpdateUI();
    }

    public void LoseLife()
    {
        _lives = Mathf.Max(0, _lives - 1);
        UpdateUI();

        if (_lives <= 0)
            GameOver();
    }

    void UpdateUI()
    {
        if (livesText != null)
            livesText.text = _lives.ToString();
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
    }
}