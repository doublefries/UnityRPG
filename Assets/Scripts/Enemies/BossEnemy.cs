using UnityEngine;

public class BossEnemy : MeleeEnemy
{
    // Animation
    [SerializeField] private Animator animator;
    [SerializeField] private string fightStartedParam = "isAttacking";

    // Projectile Attack
    [SerializeField] private FinalBossProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootCooldown = 1.5f;
    [SerializeField] private float projectileRange = 8f;

    private bool fightStarted = false;
    private float lastShotTime;

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
        TryShoot();
    }

    private void TryShoot()
    {
        Debug.Log("TryShoot called");

        if (playerTarget == null || projectilePrefab == null || firePoint == null)
            return;

        float dist = Vector2.Distance(transform.position, playerTarget.position);

        if (dist > projectileRange)
            return;

        if (Time.time - lastShotTime < shootCooldown)
            return;

        Vector2 direction = (playerTarget.position - firePoint.position).normalized;
        
        Debug.Log("Boss fired projectile");
        FinalBossProjectile projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Debug.Log("Projectile spawned");
        projectile.Initialize(direction);

        lastShotTime = Time.time;
    }

    public override void Die()
    {
        if (animator != null)
            animator.SetBool(fightStartedParam, false);

        base.Die();
    }
}