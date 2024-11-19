using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionnaireDeJeu : MonoBehaviour
{
    private static float _delaiEvenementMinS = 1.5f;
    private static float _delaiEvenementMaxS = 5f;
    private static List<GameObject> _listeObjects = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Activable");

        // Ajouter chaque objet à la liste
        foreach (GameObject obj in objects)
        {
            _listeObjects.Add(obj);
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

    private static float _ChoisirNombre(float min, float max)
    {
        return Random.Range(min, max);
    }

    public void AjouterActivable(GameObject prop) //Ajoute un objet dans la liste
    {
        _listeObjects.Add(prop);
    }

    public void SupprimerActivable(GameObject prop) //Supprime un objet de la liste
    {
        _listeObjects.Remove(prop);
    }

    private static GameObject _ChoisirObjet(List<GameObject> listeObjets)//choisi un objets aléatoirement dans la liste des objets
    {
        if (listeObjets.Count > 0) //Vérifie si la liste contient quelques chose sinon return une erreur
        {
            int randomIndex = Random.Range(0, listeObjets.Count); //Choisis un index aléatoire dont le max et la longueur de la liste
            return listeObjets[randomIndex]; //Renvoie l'objet qui correspond a l'index aléatoire
        }

        else
        {
            Debug.LogWarning("La liste des objets est vide");
            return null;
        }
    }

    public void ActiverEvenement()
    {
        
        private GameObject _prop = _ChoisirObjet(_listeObjects);
        private float _delai = _ChoisirNombre(_delaiEvenementMinS, _delaiEvenementMaxS);
        
        
    }
}

private GameObject 
