using UnityEngine;

// Temporary script so the player can damage enemies during testing
// Press Space to deal damage to the nearest enemy in range
// Delete this once your team's real combat system is in place
public class Level4PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.5f;

    private float lastAttackTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastAttackTime < attackCooldown) return;
            lastAttackTime = Time.time;

            // Find all colliders in range
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (Collider2D hit in hits)
            {
                // Skip self
                if (hit.gameObject == gameObject) continue;

                IDamageable target = hit.GetComponent<IDamageable>();
                if (target != null)
                {
                    target.TakeDamage(attackDamage);
                    Debug.Log($"Hit {hit.gameObject.name} for {attackDamage} damage");
                    break; // Only hit one target per attack
                }
            }
        }
    }
}
