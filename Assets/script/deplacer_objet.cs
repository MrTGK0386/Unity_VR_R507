using UnityEngine;

public class SquareSlideMovement : MonoBehaviour
{
    public float targetX = 5f; 
    public float slideSpeed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime = 0f;
    private bool isSliding = false;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        // Si le carré n'est pas en train de se déplacer, on attend que l'utilisateur appuie sur la barre d'espace pour lancer le déplacement
        if (!isSliding && Input.GetKeyDown(KeyCode.Space))
        {
            isSliding = true;
        }

        // Si le carré est en train de se déplacer, on le fait glisser jusqu'à la position cible
        if (isSliding)
        {
            elapsedTime += Time.deltaTime * slideSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);

            // Quand le carré atteint la position cible, on arrête le déplacement
            if (transform.position.x >= targetPosition.x)
            {
                transform.position = targetPosition;
                isSliding = false;
                elapsedTime = 0f;
            }
        }
    }
}