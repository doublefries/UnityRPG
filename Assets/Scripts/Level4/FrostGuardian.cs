using UnityEngine;
using UnityEngine.SceneManagement;

public class FrostGuardian : EnemyBase
{
    [Header("Boss Settings")]
    [SerializeField] private string nextScene = "isometricScene";
    [SerializeField] private int levelNumber = 4;
    [SerializeField] private GameObject portalPrefab;

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

    public override void Die()
    {
        Debug.Log("Frost Guardian defeated!");

        // Complete level in progression system
        if (ProgressionSystem.Instance != null)
        {
            ProgressionSystem.Instance.CollectIngredient();
            ProgressionSystem.Instance.CompleteLevel(levelNumber);
        }

        // Spawn portal where boss died
        if (portalPrefab != null)
        {
            Instantiate(portalPrefab, transform.position, Quaternion.identity);
        }

        base.Die();
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