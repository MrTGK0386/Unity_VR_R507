using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandsControllerManager : MonoBehaviour
{
    public InputActionReference triggerActionReference;
    public InputActionReference gripActionReference;
    public Animator handAnimator;
    
    private void Awake()
    {
        handAnimator = GetComponent<Animator>();
        SetupInputActions();
    }

    public void SetupInputActions()
    {
        if(triggerActionReference!=null && gripActionReference!=null){
            triggerActionReference.action.performed += ctx => UpdateHandAnimation("Trigger", ctx.ReadValue<float>());
            triggerActionReference.action.canceled += ctx => UpdateHandAnimation("Trigger", 0);
            gripActionReference.action.performed += ctx => UpdateHandAnimation("Grip", ctx.ReadValue<float>());
            gripActionReference.action.canceled += ctx => UpdateHandAnimation("Grip", 0);
        } else{
            Debug.LogWarning("Input Action References are not set in the Inspector");
        }
    }

    private void UpdateHandAnimation(string parameterName, float value)
    {
        if(handAnimator!=null){
            handAnimator.SetFloat(parameterName, value);
        }
    }

    private void OnEnable()
    {
        triggerActionReference?.action.Enable();
        gripActionReference?.action.Enable();
    }

    private void onDisable()
    {
        triggerActionReference?.action.Disable();
        gripActionReference?.action.Disable();
    }

    // SELECT TO LAUNCH PROP SCRIPT
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("in OnSelectEntered");
        // Récupère le composant Props de l'objet sélectionné
        Props propsScript = args.interactableObject.transform.GetComponent<Props>();
        Projecteur projecteurScript = args.interactableObject.transform.GetComponent<Projecteur>();
        
        // Si l'objet a bien le script Props, on appelle sa fonction
        if (propsScript != null)
        {
            Debug.Log("search SelectionObject in propsScript");
            propsScript.SelectionObject();
        }
        else if(projecteurScript!= null)
        {
            projecteurScript.SelectionObject();
        }
    }
}
