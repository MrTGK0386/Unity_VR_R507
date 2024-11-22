using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip startSound;  // Son joué au démarrage si GameTime > 60
    public AudioClip warningSound;  // Son joué 10 secondes avant la fin
    private AudioSource audioSource;

    private float startTime;  // Moment où la partie commence
    private float gameTime;   // Durée totale de la partie

    void Start()
    {
        // Vérifier si le GameManager existe via son singleton
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance est introuvable !");
            return;
        }

        // Récupère la durée totale de la partie
        gameTime = GameManager.Instance.GameTime;
        if (gameTime <= 0)
        {
            Debug.LogError("GameTime doit être supérieur à 0 !");
            return;
        }

        // Enregistre l'heure de début de la partie
        startTime = Time.time;

        // Initialisation de l'AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource manquant sur l'objet !");
            return;
        }

        // Joue le son de démarrage si GameTime > 60
        if (gameTime > 60f && startSound != null)
        {
            audioSource.PlayOneShot(startSound);
        }

        // Lancer la vérification pour jouer le son d'avertissement
        float warningTime = gameTime - 10f; // Temps avant la fin pour le son d'avertissement
        if (warningTime > 0f)
        {
            Invoke(nameof(PlayWarningSound), warningTime);
        }
    }

    private void PlayWarningSound()
    {
        if (audioSource != null && warningSound != null)
        {
            audioSource.PlayOneShot(warningSound);
        }
    }
}