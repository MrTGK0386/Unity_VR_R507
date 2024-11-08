using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionnaireDeJeu : MonoBehaviour
{
    private float _delaiEvenement = 1.5f;
    private List<GameObject> listeObjects = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Activable");

        // Ajouter chaque objet à la liste
        foreach (GameObject obj in objects)
        {
            listeObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PerdreJeu()
    {
        
    }

    private void GagnerJeu()
    {
        
    }

    private void ChoisirObjet(List<GameObject> listeObjets)
    {
        
    }

    public void ActiverEvenement()
    {
        ChoisirObjet(listeObjects);
    }
}
