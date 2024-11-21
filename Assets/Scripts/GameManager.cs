using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

//using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TMP_Text MessageText;        // Référence au texte pour afficher les messages
    public float GameTime = 6f;        // Durée d'une partie en secondes
    public float DelaiEvenementMinS = 1.5f;
    public float DelaiEvenementMaxS = 5f;
    public float tempLimiteObjet = 10f;
    
    private float _currentTime;          // Temps restant
    private bool _isGameOver = false;
    private static List<GameObject> _listeObjects = new List<GameObject>();
    private static List<GameObject> _listeLights = new List<GameObject>();    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (!_isGameOver)
        {
            // Décrémenter le temps
            _currentTime -= Time.deltaTime;

            // Vérifier si le temps est écoulé
            if (_currentTime <= 0)
            {
                EndGame(true);  // Fin de partie par temps écoulé
            }

            // Vérifier si le score est négatif
            if (ScoreManager.Instance.GetScore() < 0)
            {
                EndGame(false);  // Fin de partie par score négatif
            }
        }
    }

    private void EndGame(bool timeUp)
    {
        _isGameOver = true;
        
        if (timeUp)
        {
            // Message pour temps écoulé
            MessageText.text = "Vous avez passé la nuit suivante";
        }
        else
        {
            // Message pour score négatif
            MessageText.text = "Trop d'anomalies, vous êtes mort";
        }

        // Attendre 2 secondes avant de réinitialiser
        // Gérer la fin de la game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Invoke(nameof(ResetGame), 5f);
    }

    private void ResetGame()
    {
        // Réinitialiser le temps
        _currentTime = GameTime;
        
        // Cacher le message
        MessageText.text = "";

        // Réinitialiser le score via ScoreManager
        ScoreManager.Instance.ResetScore();
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Activable"); //Récupère tous les objets activable de la scène

        GameObject[] lights = GameObject.FindGameObjectsWithTag("Light");

        
        // Ajouter chaque objet à la liste
        foreach (GameObject obj in objects)
        {
            if (!obj.GetComponent<XRSimpleInteractable>())
            {
                obj.AddComponent<XRSimpleInteractable>();
            }
            _listeObjects.Add(obj);
            
        }
        
        foreach (GameObject light in lights)
        {
            _listeLights.Add(light);
        }
        // Réactiver le jeu
        _isGameOver = false;
        
        // Lance la méthode qui gère les event de façon récursive
        ActiverEvenement();
        ActiverEvenementLumiere(); 
    }

    private static GameObject _ChoisirLumiere(List<GameObject> listeLumieres)
    {
        if (listeLumieres.Count > 0)
        {
            int randomIndex = Random.Range(0, listeLumieres.Count);
            return listeLumieres[randomIndex];
        }
        else
        {
            Debug.LogWarning("La liste des lumières est vide");
            return null;
        }
    }

     public void ActiverEvenementLumiere()
    {
       GameObject _light = _ChoisirLumiere(_listeLights);
        if (_light == null)
        {
            return;
        }
        
        float _delai = _ChoisirNombre(DelaiEvenementMinS, DelaiEvenementMaxS);
        
        //Récupération du script de la prop
        Projecteur projScript = _light.GetComponent<Projecteur>();
        //Lancement du glissement avec le délai
        StartCoroutine(GererAllumerlight(projScript, _delai));
    }

    public void ActiverEvenement()
    {
        GameObject _prop = _ChoisirObjet(_listeObjects);
        if (_prop == null)
        {
            return;
        }
        
        float _delai = _ChoisirNombre(DelaiEvenementMinS, DelaiEvenementMaxS);
        
        //Récupération du script de la prop
        Props propScript = _prop.GetComponent<Props>();
        //Lancement du glissement avec le délai
        StartCoroutine(GererDelaiGlissement(propScript, _delai));
    
    }


    private static GameObject _ChoisirObjet(List<GameObject> listeObjets) //choisi un objets aléatoirement dans la liste des objets
    {
        if (listeObjets.Count > 0) //Vérifie si la liste contient quelques chose sinon return une erreur
        {
            int randomIndex =
                Random.Range(0, listeObjets.Count); //Choisis un index aléatoire dont le max et la longueur de la liste
            return listeObjets[randomIndex]; //Renvoie l'objet qui correspond a l'index aléatoire
        }
        else
        {
            Debug.LogWarning("La liste des objets est vide");
            return null;
        }
    }

    private IEnumerator GererDelaiGlissement(Props propScript , float _delai)
    {
        yield return new WaitForSeconds(_delai);
        propScript.DemarrerGlissement();
        if (!_isGameOver)
        {
            ActiverEvenement();
        } 
    }
    

        private IEnumerator GererAllumerlight(Projecteur projScript , float _delai)
        {
            yield return new WaitForSeconds(_delai);
            projScript.ActiverAllumer();
            if (!_isGameOver)
            {
                ActiverEvenementLumiere();
            }
        }
    private static float _ChoisirNombre(float min, float max)
    {
        return Random.Range(min, max);
    }
    
    public static void AjouterActivable(GameObject prop) //Ajoute un objet dans la liste
    {
        _listeObjects.Add(prop);
    }

    public static void SupprimerActivable(GameObject prop) //Supprime un objet de la liste
    {
        _listeObjects.Remove(prop);
    }

    public static void AjouterLight(GameObject prop)
    {
        _listeLights.Add(prop);
    }

    public static void SupprimerLight(GameObject prop)
    {
        _listeLights.Remove(prop);
    }
}