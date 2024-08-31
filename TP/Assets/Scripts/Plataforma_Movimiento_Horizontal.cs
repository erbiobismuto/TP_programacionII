using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 1.0f; 
    public float moveDistance = 2.0f; 

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position; 
    }

    void Update()
    {
        float moveDirection = movingRight ? 1 : -1;
        transform.Translate(Vector3.right * moveSpeed * moveDirection * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
        {
            movingRight = !movingRight; 
            startPosition = transform.position; 
        }
    }
}
