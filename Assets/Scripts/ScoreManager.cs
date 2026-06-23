using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text comboMultText;

    private int _score = 0;

    private int _combo = 0;

    private int scoreMultiplier = 1;

    private int lastScore = 0;

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
        if (amount < 1)
        {
            ResetCombo();
        }
        else { 
            _combo += 1;
            ManageMultiplier();
        }
        comboText.text = _combo.ToString();
        //scoretext should be like 0285 or 0085, so we need to add leading zeros
        scoreText.text = _score.ToString("D4");
        Debug.Log($"Score updated: {_score}");
    }

    public void ResetScore()
    {
        lastScore = _score;
        _score = 0;
        scoreText.text = "0000";
    }

    public void ResetCombo()
    {
        _combo = 0;
        comboText.text = "0000";
        ManageMultiplier();
    }


    public void ManageMultiplier()
    {
        int oldMultiplier = scoreMultiplier;

        if (_combo < 15) scoreMultiplier = 1;
        else if (_combo >= 15 && _combo < 30) scoreMultiplier = 2;
        else if (_combo >= 30 && _combo < 45) scoreMultiplier = 3;
        else if (_combo >= 45 && _combo < 60) scoreMultiplier = 4;
        else if (_combo >= 60) scoreMultiplier = 5;

        if (scoreMultiplier == 1)
        {
            comboMultText.gameObject.SetActive(false);
        }
        else
        {
            comboMultText.gameObject.SetActive(true);
            comboMultText.text = "x" + scoreMultiplier.ToString();

            switch (scoreMultiplier)
            {
                case 2: comboMultText.color = Color.yellow; break;
                case 3: comboMultText.color = new Color(1f, 0.5f, 0f); break;
                case 4: comboMultText.color = Color.red; break;
                case 5: comboMultText.color = Color.magenta; break;
            }

            if (scoreMultiplier > oldMultiplier)
            {
                StopCoroutine("PopEffect");
                StartCoroutine("PopEffect");
            }
        }
    }

    private IEnumerator PopEffect()
    {
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = new Vector3(1.3f, 1.3f, 1.3f);
        float duration = 0.15f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            comboMultText.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            comboMultText.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        comboMultText.transform.localScale = originalScale;
    }

    public int GetLastScore() { return lastScore; }
}
