using UnityEngine;

public class SquareSlideMovement : MonoBehaviour
{
    public float targetX = 5f; 
    public float slideSpeed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime = 0f;
    private bool isSliding = false;
    private float waitTime; 
    private bool isWaiting = true; 

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        waitTime = Random.Range(1, 4);
        Debug.Log("Temps d'attente aléatoire : " + waitTime + " secondes");
    }

    private void Update()
    {
        if (isWaiting)
        {
            waitTime -= Time.deltaTime; 
            if (waitTime <= 0f)
            {
                isWaiting = false;
                isSliding = true;
            }
            return; 
        }

        if (isSliding)
        {
            elapsedTime += Time.deltaTime * slideSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);

            if (transform.position.x >= targetPosition.x)
            {
                transform.position = targetPosition;
                isSliding = false;
                elapsedTime = 0f;
            }
        }
    }
}
