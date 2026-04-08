using UnityEngine;

public class IceEnemy : EnemyBase
{
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
            rb.MovePosition(rb.position + direction * stats.moveSpeed * Time.deltaTime);
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
            if (target == null) target = playerTarget.GetComponentInParent<IDamageable>();
            if (target != null) Attack(target);
        }
    }
}