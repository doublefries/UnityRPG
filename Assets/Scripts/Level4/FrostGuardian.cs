using UnityEngine;

// Mini boss for Level 4 — two combat phases
// Phase 1: slow and predictable
// Phase 2: triggers at half health, faster and more aggressive
public class FrostGuardian : EnemyBase
{
    [Header("Phase Settings")]
    // Health percentage that triggers phase 2 (0.5 = 50% health)
    [SerializeField] private float phase2Threshold = 0.5f;
    // Speed multiplier when entering phase 2
    [SerializeField] private float phase2SpeedMultiplier = 1.8f;
    // Cooldown multiplier in phase 2 (lower = attacks faster)
    [SerializeField] private float phase2CooldownMultiplier = 0.5f;

    private float lastAttackTime;
    private bool playerInRange = false;
    private bool isPhase2 = false;
    private float currentMoveSpeed;
    private float currentCooldown;

    // Level4Manager subscribes to this to know when to spawn the portal
    public event System.Action OnBossDefeated;

    protected override void Awake()
    {
        base.Awake();
        currentMoveSpeed = stats.moveSpeed;
        currentCooldown = AttackCooldown;
    }

    protected override void HandleDetection()
    {
        if (playerTarget == null) return;
        float dist = Vector2.Distance(transform.position, playerTarget.position);
        playerInRange = dist <= AttackRange;

        // Check if health dropped below threshold — switch to phase 2
        if (!isPhase2 && CurrentHealth <= MaxHealth * phase2Threshold)
        {
            EnterPhase2();
        }
    }

    private void EnterPhase2()
    {
        isPhase2 = true;
        currentMoveSpeed = stats.moveSpeed * phase2SpeedMultiplier;
        currentCooldown = AttackCooldown * phase2CooldownMultiplier;
        Debug.Log("Frost Guardian enters Phase 2!");
    }

    protected override void HandleMovement()
    {
        if (playerTarget == null || playerInRange) return;

        float dist = Vector2.Distance(transform.position, playerTarget.position);
        if (dist <= stats.detectionRange)
        {
            Vector2 direction = (playerTarget.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * currentMoveSpeed * Time.deltaTime);
        }
    }

    public override void Attack(IDamageable target)
    {
        if (Time.time - lastAttackTime < currentCooldown) return;
        target.TakeDamage(AttackDamage);
        lastAttackTime = Time.time;
    }

    // Override Die to fire the boss defeated event before destroying
    public override void Die()
    {
        OnBossDefeated?.Invoke();
        base.Die();
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
