using UnityEngine;
using System.Collections;

public class Projecteur : MonoBehaviour
{
    // Référence à la lumière
    private Light maLumiere;
    
    // Temps minimum et maximum pour le délai aléatoire (en secondes)
    
    public bool allumer = false;
    
    private float TempLimite;
    private Coroutine defaiteTimer;


    void Start()
    {
        StopAllCoroutines();
        TempLimite = GameManager.Instance.tempLimiteObjet;
        GameManager.AjouterLight(this.gameObject);
        
        // Récupérer le composant Light attaché à l'objet
        maLumiere = this.GetComponentInChildren<Light>();
        
        // S'assurer que la lumière est éteinte au départ
        if (maLumiere != null)
        {
            maLumiere.enabled = false;
        }
        else
        {
            Debug.LogError("Aucun composant Light n'a été trouvé sur cet objet!");
        }
    }
    
    void Update(){
    }
    

    public void ActiverAllumer(){
        allumer = true;
        AllumerApresDelai();
        GameManager.SupprimerLight(this.gameObject);
    }

    private void AllumerApresDelai()
    {

        maLumiere.enabled = true;
        
        if (defaiteTimer == null)
        {
            defaiteTimer = StartCoroutine(DefaiteTimer());
        }
        
    }

    public void SelectionObject()
    {
        if (maLumiere.enabled)
        {
            maLumiere.enabled = false;
            allumer = false;
            ScoreManager.Instance.AddPoints(1);
            GameManager.AjouterLight(this.gameObject);
            
            if (defaiteTimer != null)
            {
                StopCoroutine(defaiteTimer);
                defaiteTimer = null;
            }
        }
    }

     private IEnumerator DefaiteTimer()
    {
        while (maLumiere.enabled)
        {
            yield return new WaitForSeconds(TempLimite);
            
            if (maLumiere.enabled)
            {
                ScoreManager.Instance.SubtractPoints(1);
                //Debug.Log("Point soustrait : objet non remis en place");
            }
        }
    }

}