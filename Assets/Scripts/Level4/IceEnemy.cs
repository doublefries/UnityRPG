using UnityEngine;

using UnityEngine;

// Ice cave enemy — chases and attacks the player
// Has slide resistance so it moves more confidently on ice than the player
public class IceEnemy : EnemyBase
{
    // 0 = moves perfectly on ice, 1 = fully affected by ice
    [SerializeField] private float slideResistance = 0.3f;

    private float lastAttackTime;
    private bool playerInRange = false;

    protected override void HandleDetection()
    {
        if (playerTarget == null) return;
        float dist = Vector2.Distance(transform.position, playerTarget.position);
        playerInRange = dist <= AttackRange;
    }

    protected override void HandleMovement()
    {
        if (playerTarget == null || playerInRange) return;

        float dist = Vector2.Distance(transform.position, playerTarget.position);
        if (dist <= stats.detectionRange)
        {
            Vector2 direction = (playerTarget.position - transform.position).normalized;
            // Ice enemies are adapted to ice — slideResistance reduces friction effect
            float effectiveSpeed = stats.moveSpeed * (1f - slideResistance);
            rb.MovePosition(rb.position + direction * effectiveSpeed * Time.deltaTime);
        }
    }

    public override void Attack(IDamageable target)
    {
        if (Time.time - lastAttackTime < AttackCooldown) return;
        target.TakeDamage(AttackDamage);
        lastAttackTime = Time.time;
    }

    protected override void Update()
    {
        base.Update();

        if (playerInRange && playerTarget != null)
        {
            IDamageable target = playerTarget.GetComponent<IDamageable>();
            if (target != null)
                Attack(target);
        }
    }
}
