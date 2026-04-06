using UnityEngine;

public class RisingLava: MonoBehaviour
{
    [SerializeField] private float initialRiseSpeed = 0.4f;
    [SerializeField] private float exponentialRate = 0.15f;
    [SerializeField] private float maxRiseSpeed = 4f;

    private Rigidbody2D rb;
    private float elapsedTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() //make lava increase speed exponentially as level continues
    {
        elapsedTime += Time.fixedDeltaTime;

        float currentSpeed = initialRiseSpeed * Mathf.Exp(exponentialRate * elapsedTime);
        currentSpeed = Mathf.Min(currentSpeed, maxRiseSpeed);

        rb.MovePosition(rb.position + Vector2.up * currentSpeed * Time.fixedDeltaTime);
    }
    
    
}