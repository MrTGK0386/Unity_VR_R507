using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MovementSoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private Vector3 lastPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // Vérifie si la position a changé (mouvement détecté)
        if (transform.position != lastPosition)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Joue le son si ce n'est pas déjà le cas
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Arrête le son si l'objet ne bouge plus
            }
        }

        // Met à jour la dernière position
        lastPosition = transform.position;
    }
}