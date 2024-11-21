using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightInteraction : MonoBehaviour
{
    private XRController otherHandController;
    private XRRayInteractor otherHandRayInteractor;

    private void Start()
    {
        // Récupérez une référence au contrôleur et au XRRayInteractor de l'autre main
        otherHandController = GetOtherHandController();
        if (otherHandController != null)
        {
            otherHandRayInteractor = otherHandController.GetComponent<XRRayInteractor>();
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Activez le XRRayInteractor de l'autre main et ajustez sa position
        if (otherHandRayInteractor != null)
        {
            otherHandRayInteractor.enabled = true;
            otherHandRayInteractor.transform.position = otherHandController.transform.position;
            otherHandRayInteractor.transform.rotation = otherHandController.transform.rotation;
        }
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        // Désactivez le XRRayInteractor de l'autre main
        if (otherHandRayInteractor != null)
        {
            otherHandRayInteractor.enabled = false;
        }
    }

    private XRController GetOtherHandController()
    {
        // Recherchez l'autre contrôleur de main
        XRController[] controllers = FindObjectsOfType<XRController>();
        foreach (XRController controller in controllers)
        {
            if (controller.gameObject != this.gameObject.transform.parent.gameObject)
            {
                return controller;
            }
        }
        return null;
    }
}