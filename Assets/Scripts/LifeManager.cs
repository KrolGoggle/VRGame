using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance { get; private set; }

    [Header("UI")]
    public TMP_Text livesText;

    [Header("Ustawienia")]
    public int maxLives = 5;

    private int _lives;

    public MenuManager menu;
    public SpawnerLogic spawner;
    public ScoreManager score;

    public AudioClip clipGameOver;

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
        spawner.StopSpawning();
        menu.Show();
        AudioSource.PlayClipAtPoint(clipGameOver, new Vector3(0f,0f,0f), 0.5f);
        _lives = maxLives; // Reset lives for next game
        score.ResetScore(); // Reset score for next game
    }
}