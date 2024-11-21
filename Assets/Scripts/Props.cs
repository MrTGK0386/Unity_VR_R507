using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Props : MonoBehaviour
{
    public float vitesseGlissement = 2f;
    public bool Glisse = false;
    public GameObject NavMeshTarget; 
    
    private Vector3 positionDepart;
    private Quaternion rotationDepart;
    private Vector3 positionDepartpasagent;
    private Quaternion rotationDepartpasagent;
    [SerializeField] private Vector3 positionCible;
    private float tempsEcoule = 0f;
    private bool departposition = true;
    private Coroutine defaiteTimer;
    private float TempLimite;
    private NavMeshAgent agent;
    
    private void Start()
    {
        TempLimite = GameManager.Instance.tempLimiteObjet;
        StopAllCoroutines();
        InitialiserPositions();
    }

    private void Update()
    {
    }

    private void InitialiserPositions()
    {
        positionDepartpasagent = transform.localPosition;
        rotationDepartpasagent = transform.rotation;

        positionDepart = transform.position;
        rotationDepart = transform.rotation;
        Debug.Log("initiating positions..."  + positionDepart);

        if (this.GetComponent<NavMeshAgent>())
        {
            agent = this.GetComponent<NavMeshAgent>();
        }
    }

    public void DemarrerGlissement()
    {
        Glisse = true;
        GlisserVersCible();
        GameManager.SupprimerActivable(this.gameObject);
    }
    

    private void GlisserVersCible()
    {
        Debug.Log($"{this.gameObject.name} se déplace");

        if (agent != null)
        {
            agent.destination = NavMeshTarget.transform.position;
        }
        else
        {
            StartCoroutine(DeplacementLisse());
        }
        
        IEnumerator DeplacementLisse()
        {
            tempsEcoule = 0f;
            while (Vector3.Distance(transform.localPosition, positionCible) > 0.01f)
            {
                tempsEcoule += Time.deltaTime * vitesseGlissement;
                transform.localPosition = Vector3.Lerp(positionDepart, positionCible, tempsEcoule);
                yield return null;  // Attendre la prochaine frame
            }

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
        transform.localPosition = positionCible;
        Glisse = false;
        tempsEcoule = 0f;
    }

    public void SelectionObject()
    {
        Debug.Log(departposition);

        if (agent)
        {
            Debug.Log("positionDepart =" + positionDepart);

            agent.Warp(positionDepart);
            transform.rotation = rotationDepart;

            // Dans votre script
            if (!GameManager._listeObjects.Contains(this.gameObject))
            {
                ScoreManager.Instance.AddPoints(1);
            }

            GameManager.AjouterActivable(this.gameObject);
            
            if (defaiteTimer != null)
            {
                StopCoroutine(defaiteTimer);
                defaiteTimer = null;
            }
        }

        if (!departposition)
        {        
            // Fallback si pas de NavMeshAgent
            Debug.Log("TIRROIR");
            transform.localPosition = positionDepartpasagent;
            transform.rotation = rotationDepartpasagent;

            Glisse = false;
            tempsEcoule = 0f;
            departposition = true;
            ScoreManager.Instance.AddPoints(1);
            GameManager.AjouterActivable(this.gameObject);
            
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