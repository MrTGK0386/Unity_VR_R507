using UnityEngine;

public class MouvementGlissant : MonoBehaviour
{
    public float cibleX;                
    public float vitesseGlissement = 2f; 

    private Vector3 positionDepart;     
    private Vector3 positionCible;      
    private float tempsEcoule = 0f;     
    private bool estEnGlissement = false; 
    private float tempsAttente;       

    private void Start()
    {
        InitialiserPositions();   
        DefinirTempsAttenteAleatoire(); 
    }

    private void Update()
    {
        if (estEnGlissement) 
        {
            GlisserVersCible(); 
        }
    }

    private void InitialiserPositions()
    {
        positionDepart = transform.position;
        positionCible = new Vector3(cibleX, transform.position.y, transform.position.z); 
    }

    private void DefinirTempsAttenteAleatoire()
    {

        tempsAttente = Random.Range(1, 4);
        Debug.Log("Temps d'attente aléatoire : " + tempsAttente + " secondes");
        Invoke(nameof(DemarrerGlissement), tempsAttente);
    }

    private void DemarrerGlissement()
    {
        estEnGlissement = true; 
    }

    private void GlisserVersCible()
    {
        tempsEcoule += Time.deltaTime * vitesseGlissement; 
        transform.position = Vector3.Lerp(positionDepart, positionCible, tempsEcoule); 

        if (transform.position.x >= positionCible.x)
        {
            ArreterGlissement(); 
        }
    }

    private void ArreterGlissement()
    {
        transform.position = positionCible; 
        estEnGlissement = false; 
        tempsEcoule = 0f; 
    }
}
