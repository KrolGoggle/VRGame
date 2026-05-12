using UnityEngine;
using TMPro;

public class TestLifeManager : MonoBehaviour
{
    public static TestLifeManager Instance { get; private set; }

    [Header("UI")]
    public TMP_Text livesText;

    [Header("Ustawienia")]
    public int maxLives = 6;

    private int _lives;

    public TestMenuManager menu;
    public TestSpawnerLogic spawner;
    public TestScoreManager score;

    public AudioClip clipGameOver;

    private bool _losingLife = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ResetLives()
    {
        _lives = maxLives;
        UpdateUI();
    }

    void Start()
    {
        _lives = maxLives;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (_losingLife) return;
        _losingLife = true;

        _lives = Mathf.Max(0, _lives - 1);
        Debug.Log($"Live lost: {_lives}");
        UpdateUI();

        if (_lives <= 0)
            GameOver();

        _losingLife = false;
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
        UpdateUI();
    }
}