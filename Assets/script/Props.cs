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

    public Score scoreScript;

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
        tempsAttente = Random.Range(1, 4);
        Debug.Log("Temps d'attente aléatoire : " + tempsAttente + " secondes");
        Invoke(nameof(DemarrerGlissement), tempsAttente);
    }

    private void DemarrerGlissement()
    {
        estEnGlissement = true;
    }
    private void defaite()
    {
        Score.SubtractPoints(1);
         if (scoreScript != null)
            {
                Score.RefreshScoreDisplay(scoreScript);
            }
    }


    private void GlisserVersCible()
    {
        tempsEcoule += Time.deltaTime * vitesseGlissement;
        transform.position = Vector3.Lerp(positionDepart, positionCible, tempsEcoule);

        if (transform.position.x >= positionCible.x)
        {
            ArreterGlissement();
            departposition = false;
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
        Debug.Log("verifie le changement");

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            Debug.Log("verifie le changement 2");

            if (departposition == false)
            {
                transform.position = positionDepart;
                estEnGlissement = false;
                tempsEcoule = 0f;
                departposition = true;
                Debug.Log("verifie le changement 3");
                Score.AddPoints(1);
                InitialiserPositions();
                DefinirTempsAttenteAleatoire();

                // Stopper le timer si l'objet est cliqué
                if (defaiteTimer != null)
                {
                    StopCoroutine(defaiteTimer);
                    defaiteTimer = null;
                }
            }
            else
            {
                // Ne rien faire, l'objet n'a pas bougé
            }

            if (scoreScript != null)
            {
                Score.RefreshScoreDisplay(scoreScript);
            }
        }

        // Démarrer le timer si l'objet a bougé
        if (departposition == false)
        {
            defaiteTimer = StartCoroutine(DefaiteTimer());
        }
    }

    private IEnumerator DefaiteTimer()
    {
        yield return new WaitForSeconds(10f);
        Score.SubtractPoints(1);
        if (scoreScript != null)
        {
            Score.RefreshScoreDisplay(scoreScript);
        }
    }

}