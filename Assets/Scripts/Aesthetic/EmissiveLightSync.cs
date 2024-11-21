using UnityEngine;

[RequireComponent(typeof(Light))]
public class EmissiveLightSync : MonoBehaviour
{
    [SerializeField] private Material emissiveMaterial;
    private Light lightComponent;
    private Color emissionColor;
    
    private void Start()
    {
        lightComponent = GetComponent<Light>();
        // Sauvegarde la couleur d'émission configurée dans Unity
        emissionColor = emissiveMaterial.GetColor("_EmissionColor");
    }

    private void Update()
    {
        // Active/désactive l'émission en fonction de l'état de la lumière
        emissiveMaterial.SetColor("_EmissionColor", lightComponent.enabled ? emissionColor : Color.black);
    }
}