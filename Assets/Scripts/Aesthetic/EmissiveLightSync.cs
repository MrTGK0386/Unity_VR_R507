using UnityEngine;

[RequireComponent(typeof(Light))]
public class EmissiveLightSync : MonoBehaviour
{
    [SerializeField] private Material emissiveMaterial;
    private Light lightComponent;
    private Color originalEmissionColor;
    
    private void Awake()
    {
        // Sauvegarder la couleur d'émission originale au tout début
        originalEmissionColor = emissiveMaterial.GetColor("_EmissionColor");
    }
    
    private void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    private void Update()
    {
        // Activer/désactiver l'émission en fonction de l'état de la lumière
        emissiveMaterial.SetColor("_EmissionColor", lightComponent.enabled ? originalEmissionColor : Color.black);
    }

    private void OnDisable()
    {
        // Restaurer la couleur originale quand le script est désactivé
        emissiveMaterial.SetColor("_EmissionColor", originalEmissionColor);
    }

    private void OnApplicationQuit()
    {
        // Restaurer la couleur originale quand l'application se ferme
        emissiveMaterial.SetColor("_EmissionColor", originalEmissionColor);
    }
}