using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;  // Assurez-vous de lier cet élément dans l'inspecteur
    private static int score = 0; // Score initial de 50

    void Start()
    {
        UpdateScoreText(); // Affiche le score de départ
    }

    // Méthode pour augmenter le score
    public static void AddPoints(int points)
    {
        score += points;
        Debug.Log("Score augmenté : " + score);
    }

    // Méthode pour diminuer le score
    public static void SubtractPoints(int points)
    {
        score -= points;
        Debug.Log("Score diminué : " + score);
    }

    // Met à jour l'affichage du score
    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score.ToString();
        }
    }

    // Méthode pour actualiser l'affichage du score depuis l'extérieur
    public static void RefreshScoreDisplay(Score scoreInstance)
    {
        scoreInstance.UpdateScoreText();
    }

    public static int GetScore()
{
    return score;
}
}
