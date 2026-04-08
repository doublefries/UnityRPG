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
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Vector2 direction, GameObject owner)
    {
        moveDirection = direction.normalized;
        
        Collider2D[] projectileColliders = GetComponentsInChildren<Collider2D>();
        Collider2D[] ownerColliders = owner.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D projectileCol in projectileColliders)
        {
            foreach (Collider2D ownerCol in ownerColliders)
            {
                Physics2D.IgnoreCollision(projectileCol, ownerCol, true);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
}