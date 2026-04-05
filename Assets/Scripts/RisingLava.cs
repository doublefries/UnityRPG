using UnityEngine;

public class RisingLava: MonoBehaviour
{
    [SerializeField] private float riseSpeed = 0.75f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.up * riseSpeed * Time.fixedDeltaTime);
    }
}