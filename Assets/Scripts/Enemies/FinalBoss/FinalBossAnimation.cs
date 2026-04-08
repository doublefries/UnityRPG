using UnityEngine;

public class FinalBossAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void StartAttack()
    {
        animator.SetBool("isAttacking", true);
    }

    public void StopAttack()
    {   
        animator.SetBool("isAttacking", false);
    }
}