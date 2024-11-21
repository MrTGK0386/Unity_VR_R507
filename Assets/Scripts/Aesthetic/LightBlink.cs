using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightBlink : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 1f; // Temps en secondes entre chaque clignotement
    [SerializeField] private bool startOn = true;      // État initial de la lumière
    
    private Light lightComponent;
    private float timer;
    
    private void Start()
    {
        lightComponent = GetComponent<Light>();
        lightComponent.enabled = startOn;
        timer = blinkInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            lightComponent.enabled = !lightComponent.enabled; // Inverse l'état de la lumière
            timer = blinkInterval; // Reset le timer
        }
    }
}