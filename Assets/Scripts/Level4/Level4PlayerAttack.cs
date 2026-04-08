using System.Collections.Generic;
using UnityEngine;

public class Level4PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.5f;

    private float lastAttackTime = -999f;
    private Animator animator;
    private Vector2 lastMoveDirection = Vector2.down;
    private bool isAttacking = false;

    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateLastDirection();

        if (Input.GetKeyDown(KeyCode.F))
        {
            TryStartAttack();
        }
    }

    private void UpdateLastDirection()
    {
        if (animator == null) return;

        float moveX = animator.GetFloat(MoveXHash);
        float moveY = animator.GetFloat(MoveYHash);

        Vector2 currentDirection = new Vector2(moveX, moveY);

        if (currentDirection != Vector2.zero)
        {
            lastMoveDirection = currentDirection.normalized;
        }
    }

    private void TryStartAttack()
    {
        if (animator == null) return;
        if (isAttacking) return;
        if (Time.time - lastAttackTime < attackCooldown) return;

        lastAttackTime = Time.time;
        isAttacking = true;

        animator.SetFloat(MoveXHash, lastMoveDirection.x);
        animator.SetFloat(MoveYHash, lastMoveDirection.y);
        animator.ResetTrigger(AttackHash);
        animator.SetTrigger(AttackHash);
    }

    // CALL THIS FROM THE ATTACK ANIMATION EVENT
    public void PerformAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        HashSet<IDamageable> hitTargets = new HashSet<IDamageable>();

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            DamageReceiver receiver = hit.GetComponent<DamageReceiver>();
            if (receiver != null)
            {
                IDamageable receiverTarget = hit.GetComponentInParent<IDamageable>();
                if (receiverTarget != null && !hitTargets.Contains(receiverTarget))
                {
                    receiver.ReceiveDamage(attackDamage);
                    hitTargets.Add(receiverTarget);
                    Debug.Log("Hit " + hit.gameObject.name + " for " + attackDamage + " damage");
                }
                continue;
            }

            IDamageable target = hit.GetComponent<IDamageable>();
            if (target == null) target = hit.GetComponentInParent<IDamageable>();

            if (target != null && !hitTargets.Contains(target))
            {
                target.TakeDamage(attackDamage);
                hitTargets.Add(target);
                Debug.Log("Hit " + hit.gameObject.name + " for " + attackDamage + " damage");
            }
        }
    }

    // CALL THIS FROM THE LAST FRAME OF EACH ATTACK ANIMATION
    public void EndAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}