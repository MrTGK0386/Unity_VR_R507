using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LightSoundSync : MonoBehaviour
{
    private Light targetLight;
    private AudioSource audioSource;
    private bool wasEnabled;
    void Start()
    {
        targetLight = GetComponentInChildren<Light>();
        audioSource = GetComponent<AudioSource>();
        wasEnabled = targetLight.enabled;
    }
    void Update()
    {
        if (wasEnabled != targetLight.enabled)
        {
            wasEnabled = targetLight.enabled;
            if (wasEnabled)
                audioSource.Play();
            else
                audioSource.Stop();
        }
    }
}