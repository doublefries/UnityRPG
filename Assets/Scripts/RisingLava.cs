using UnityEngine;

public class RisingLava: MonoBehaviour
{
    [SerializeField] private float initialRiseSpeed = 0.4f;
    [SerializeField] private float exponentialRate = 0.15f;
    [SerializeField] private float maxRiseSpeed = 4f;
    [SerializeField] private float maxYPosition = 15f;

    private Rigidbody2D rb;
    private float elapsedTime;
    private bool reachedMaxHeight = false; //fields to be used

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() //make lava increase speed exponentially as level continues
    {
        if (reachedMaxHeight) return;
        
        elapsedTime += Time.fixedDeltaTime;

        float currentSpeed = initialRiseSpeed * Mathf.Exp(exponentialRate * elapsedTime);
        currentSpeed = Mathf.Min(currentSpeed, maxRiseSpeed);

        Vector2 nextPosition = rb.position + Vector2.up * currentSpeed * Time.fixedDeltaTime; //calculate next position and see if we have reached it
        
        if (nextPosition.y >= maxYPosition)
        {
            nextPosition.y = maxYPosition;
            reachedMaxHeight = true;
        }

        rb.MovePosition(nextPosition);
    }
    
    
}