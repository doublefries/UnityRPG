using UnityEngine;

public class BossEnemy : MeleeEnemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private string fightStartedParam = "isAttacking";

    private bool fightStarted = false;

    public void StartFight()
    {
        Debug.Log("Boss fight started");
        
        if (fightStarted || !isAlive)
            return;

        fightStarted = true;

        if (animator != null)
            animator.SetBool(fightStartedParam, true);
    }

    protected override void Update()
    {
        if (!fightStarted || !isAlive)
            return;

        base.Update();
    }

    public override void Die()
    {
        if (animator != null)
            animator.SetBool(fightStartedParam, false);

        base.Die();
    }
}