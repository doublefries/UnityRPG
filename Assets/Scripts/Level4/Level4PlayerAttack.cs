using UnityEngine;

public class Level4PlayerAttack : MonoBehaviour
{
    [Header("Fallback Attack (no hitboxes needed)")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.5f;

    private float lastAttackTime;

    private void Update()
    {
        // F key to match your team's attack key
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Time.time - lastAttackTime < attackCooldown) return;
            lastAttackTime = Time.time;

            Debug.Log("Player attacked!");

            // Find everything in range
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject == gameObject) continue;

                IDamageable target = hit.GetComponent<IDamageable>();
                if (target == null) target = hit.GetComponentInParent<IDamageable>();

                if (target != null)
                {
                    target.TakeDamage(attackDamage);
                    Debug.Log("Hit " + hit.gameObject.name + " for " + attackDamage + " damage");
                }
            }
        }
    }

    // Draws the attack range in Scene view so you can see it
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}