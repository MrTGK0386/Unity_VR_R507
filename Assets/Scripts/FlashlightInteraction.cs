using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightController : MonoBehaviour 
{
    public GameObject whiteLight;
    private XRBaseInteractable interactable;
    private bool isOn = false;
    private bool buttonPressed = false;

    void Start()
    {
        // Récupère le XRGrabInteractable attaché à ce GameObject
        interactable = GetComponent<XRGrabInteractable>();
        
        // S'abonne à l'événement de sélection (quand la lampe est attrapée)
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnGrabbed);
        }
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        // Récupère le contrôleur qui a attrapé l'objet
        XRController controller = args.interactorObject.transform.GetComponent<XRController>();
        
        // Démarre la coroutine d'écoute du bouton pour ce contrôleur spécifique
        if (controller != null)
        {
            StartCoroutine(CheckControllerButton(controller));
        }
    }

    System.Collections.IEnumerator CheckControllerButton(XRController controller)
    {
        while (interactable.isSelected)
        {
            // Vérifie si le contrôleur qui tient l'objet appuie sur le bouton
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonValue))
            {
                if (buttonValue && !buttonPressed)
                {
                    ToggleLight();
                    buttonPressed = true;
                }
                else if (!buttonValue)
                {
                    buttonPressed = false;
                }
            }
            
            yield return null;
        }
    }

    void ToggleLight()
    {
        isOn = !isOn;
        whiteLight.SetActive(isOn);
    }
}