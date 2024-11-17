using UnityEngine;
using System.Collections;

public class Props : MonoBehaviour
{
    public float cibleX;
    public float vitesseGlissement = 2f;

    private Vector3 positionDepart;
    private Vector3 positionCible;
    private float tempsEcoule = 0f;
    private bool estEnGlissement = false;
    private float tempsAttente;
    private bool departposition = true;
    private Coroutine defaiteTimer;

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

        if (Input.GetMouseButtonDown(0))
        {
            VerifierEtGererClick();
        }
    }

    private void InitialiserPositions()
    {
        positionDepart = transform.position;
        positionCible = new Vector3(cibleX, transform.position.y, transform.position.z);
    }

    private void DefinirTempsAttenteAleatoire()
    {
        tempsAttente = Random.Range(1f, 4f);
        Debug.Log($"Temps d'attente aléatoire : {tempsAttente} secondes");
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
        estEnGlissement = false;
        tempsEcoule = 0f;
    }

    private void VerifierEtGererClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            if (!departposition)
            {
                transform.position = positionDepart;
                estEnGlissement = false;
                tempsEcoule = 0f;
                departposition = true;
                
                ScoreManager.Instance.AddPoints(1);
                
                InitialiserPositions();
                DefinirTempsAttenteAleatoire();

                if (defaiteTimer != null)
                {
                    StopCoroutine(defaiteTimer);
                    defaiteTimer = null;
                }
            }
        }
    }

    private IEnumerator DefaiteTimer()
    {
        while (!departposition)
        {
            yield return new WaitForSeconds(10f);
            
            if (!departposition)
            {
                ScoreManager.Instance.SubtractPoints(1);
                Debug.Log("Point soustrait : objet non remis en place");
            }
        }
    }
}