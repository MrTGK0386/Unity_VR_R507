using UnityEngine;
using System.Collections;

public class Projecteur : MonoBehaviour
{
    // Référence à la lumière
    private Light maLumiere;
    
    // Temps minimum et maximum pour le délai aléatoire (en secondes)
    private float tempsMinimum = 2f;
    private float tempsMaximum = 10f;
    public bool allumer = false;
    public float TempLimite = 10f;


    void Start()
    {
        StopAllCoroutines();
        // Récupérer le composant Light attaché à l'objet
        maLumiere = GetComponent<Light>();
        if (maLumiere != null)
        {
            maLumiere.enabled = false;
            
      }
        else
        {
            Debug.LogError("Aucun composant Light n'a été trouvé sur cet objet!");
        }
        
        // S'assurer que la lumière est éteinte au départ

    }
    void Update(){
        if(allumer){
            AllumerApresDelai();
        }
        if (Input.GetMouseButtonDown(0))
        {
            VerifierEtGererClick();
        }
    }
    

    public void ActiverAllumer(){
        allumer = true;
    }

    private void AllumerApresDelai()
    {

        maLumiere.enabled = true;
        
    }

    private void VerifierEtGererClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            if(maLumiere.enabled){
            maLumiere.enabled = false;
            allumer = false;
            ScoreManager.Instance.AddPoints(1);
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