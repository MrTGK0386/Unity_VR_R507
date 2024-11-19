using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public TMP_Text scoreText;
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
        Debug.Log($"Score augmenté : {score}");
    }

    public void SubtractPoints(int points)
    {
        score -= points;
        UpdateScoreText();
        Debug.Log($"Score diminué : {score}");
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score : {score}";
        }
        else
        {
            Debug.LogWarning("ScoreText reference is missing!");
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
{
    score = 0;
    UpdateScoreText();
}
}