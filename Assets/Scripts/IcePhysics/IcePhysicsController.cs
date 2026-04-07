using UnityEngine;

// Attach to the Player alongside their movement script
// When on ice: input adds force to velocity, friction gradually decelerates
// When off ice: does nothing, normal movement takes over
public class IcePhysicsController : MonoBehaviour
{
    [Header("Ice Settings")]
    // How fast the player decelerates — lower = more slippery (0.01 very icy, 0.5 barely noticeable)
    [SerializeField] private float frictionCoefficient = 0.05f;

    // How much player input affects velocity while sliding
    [SerializeField] private float slideAcceleration = 8f;

    // Maximum sliding speed so the player can't accelerate forever
    [SerializeField] private float maxSlideSpeed = 10f;

    // Public read so other scripts can check, private set so only EnterIce/ExitIce can change
    public bool IsSliding { get; private set; } = false;

    // The player's current momentum — persists between frames, creating the sliding feel
    private Vector2 velocity = Vector2.zero;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = GetComponentInParent<Rigidbody2D>();
    }

    // Called by IceZone when player steps on ice
    public void EnterIce()
    {
        IsSliding = true;
    }

    // Called by IceZone when player leaves ice
    public void ExitIce()
    {
        IsSliding = false;
        // Reset velocity so ice momentum doesn't carry onto normal ground
        velocity = Vector2.zero;
    }

    // FixedUpdate for consistent physics regardless of framerate
    private void FixedUpdate()
    {
        // When not on ice, do nothing — normal movement handles it
        if (!IsSliding) return;

        // Read WASD input, normalize so diagonal isn't faster
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(h, v).normalized;

        // += adds to velocity instead of replacing it — this is what creates momentum
        // On normal ground you'd do velocity = input * speed (instant changes)
        // On ice, velocity += input * accel (gradual changes, keeps sliding when keys released)
        if (input.magnitude > 0.1f)
        {
            velocity += input * slideAcceleration * Time.fixedDeltaTime;
        }

        // Friction: move velocity toward zero each frame
        // At 0.05, velocity retains 95% per frame — gradual deceleration
        velocity = Vector2.Lerp(velocity, Vector2.zero, frictionCoefficient);

        // Cap speed so holding a key can't accelerate forever
        if (velocity.magnitude > maxSlideSpeed)
        {
            velocity = velocity.normalized * maxSlideSpeed;
        }

        // Apply through physics so the player bounces off walls, not through them
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}