using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TMP_Text messageText;        // Référence au texte pour afficher les messages
    public float gameTime = 6f;        // Durée d'une partie en secondes
    private float currentTime;          // Temps restant
    private bool isGameOver = false;

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

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            // Décrémenter le temps
            currentTime -= Time.deltaTime;

            // Vérifier si le temps est écoulé
            if (currentTime <= 0)
            {
                EndGame(true);  // Fin de partie par temps écoulé
            }

            // Vérifier si le score est négatif
            if (ScoreManager.Instance.GetScore() < 0)
            {
                EndGame(false);  // Fin de partie par score négatif
            }
        }
    }

    private void EndGame(bool timeUp)
    {
        isGameOver = true;

        if (timeUp)
        {
            // Message pour temps écoulé
            messageText.text = "Vous avez passé la nuit suivante";
        }
        else
        {
            // Message pour score négatif
            messageText.text = "Trop d'anomalies, vous êtes mort";
        }

        // Attendre 2 secondes avant de réinitialiser
        Invoke(nameof(ResetGame), 2f);
    }

    private void ResetGame()
    {
        // Réinitialiser le temps
        currentTime = gameTime;
        
        // Réinitialiser le score via ScoreManager
        ScoreManager.Instance.ResetScore();
        
        // Cacher le message
        messageText.text = "";
        
        // Réactiver le jeu
        isGameOver = false;
    }
}