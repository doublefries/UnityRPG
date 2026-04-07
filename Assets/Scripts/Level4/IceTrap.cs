using UnityEngine;

// Timed trap that cycles between active (dangerous) and inactive (safe)
// Works with DamageDealer — this script controls the on/off cycle
// DamageDealer handles the actual damage on collision
public class IceTrap : MonoBehaviour
{
    [Header("Trap Settings")]
    // Seconds the trap stays active and dangerous
    [SerializeField] private float activeDuration = 1f;
    // Seconds the trap stays inactive and safe
    [SerializeField] private float cooldownDuration = 2f;
    // If false, trap is always active with no cycling
    [SerializeField] private bool isTimed = true;

    private Collider2D trapCollider;
    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool isActive = true;

    private void Awake()
    {
        trapCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = activeDuration;
    }

    private void Update()
    {
        // If not timed, trap is always dangerous — nothing to cycle
        if (!isTimed) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // Flip between active and inactive
            isActive = !isActive;
            timer = isActive ? activeDuration : cooldownDuration;

            // Enable/disable the collider so damage only happens when active
            trapCollider.enabled = isActive;

            // Visual feedback — dim when safe, full opacity when dangerous
            if (spriteRenderer != null)
            {
                Color c = spriteRenderer.color;
                c.a = isActive ? 1f : 0.3f;
                spriteRenderer.color = c;
            }
        }
    }
}