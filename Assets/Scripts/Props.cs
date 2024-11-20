using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class Props : MonoBehaviour
{
    public float cibleX;
    public float vitesseGlissement = 2f;
    public bool Glisse = false;
    public float TempLimite = 10f;
    
    private Vector3 positionDepart;
    private Vector3 positionCible;
    private float tempsEcoule = 0f;
    private bool departposition = true;
    private Coroutine defaiteTimer;
    
    public void DemarrerGlissement()
    {
        Glisse = true;
        GameManager.SupprimerActivable(this.gameObject);
    }
    
    private void Start()
    {
        StopAllCoroutines();
        InitialiserPositions();
    }

    private void Update()
    {
        if (Glisse)
        {
            GlisserVersCible();
        }
    }

    private void InitialiserPositions()
    {
        positionDepart = transform.position;
        positionCible = new Vector3(cibleX, transform.position.y, transform.position.z);
    }
    

    private void GlisserVersCible()
    {
        tempsEcoule += Time.deltaTime * vitesseGlissement;
        transform.position = Vector3.Lerp(positionDepart, positionCible, tempsEcoule);

        if (transform.position.x >= positionCible.x)
        {
            ArreterGlissement();
            departposition = false;
            if (defaiteTimer == null)
            {
                defaiteTimer = StartCoroutine(DefaiteTimer());
            }
        }
    }

    private void ArreterGlissement()
    {
        transform.position = positionCible;
        Glisse = false;
        GameManager.AjouterActivable(this.gameObject);
        tempsEcoule = 0f;
    }

    private void SelectionObject()
    {
        if (!departposition)
        {
            transform.position = positionDepart;
            Glisse = false;
            tempsEcoule = 0f;
            departposition = true;

            ScoreManager.Instance.AddPoints(1);

            InitialiserPositions(); // Modifier pour refaire glisser l'objet à se position initiale

            if (defaiteTimer != null)
            {
                StopCoroutine(defaiteTimer);
                defaiteTimer = null;
            }
        }
    }

    private IEnumerator DefaiteTimer()
    {
        while (!departposition)
        {
            yield return new WaitForSeconds(TempLimite);
            
            if (!departposition)
            {
                ScoreManager.Instance.SubtractPoints(1);
                //Debug.Log("Point soustrait : objet non remis en place");
            }
        }
    }
}