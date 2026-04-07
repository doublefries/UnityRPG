using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FinalBossProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Projectile Awake");
    }

    private void Start()
    {
        Debug.Log("Projectile Start. Lifetime = " + lifetime);
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
        Debug.Log("Projectile initialized. Direction = " + moveDirection);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
        Debug.Log("Projectile velocity = " + rb.linearVelocity);
    }

    private void OnDestroy()
    {
        Debug.Log("Projectile destroyed");
    }
}