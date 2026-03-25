using UnityEngine;

// subclass of EnemyBase: enemy you can place in a scene
// fill in the three abstract methods: HandleDetection, HandleMovement, Attack
public class MeleeEnemy : EnemyBase
{
    // specific to MeleeEnemy

    // tracks when the last attack happened, used with cooldown to prevent attacking every frame
    // Defaults to 0 --> the enemy can attack immediately on first contact
    private float lastAttackTime;

    // flag set by HandleDetection, read by HandleMovement and Update, figures out what can be attacked
    private bool playerInRange = false;

     // called every frame by EnemyBase.Update() to check if player is close enough to attack
    protected override void HandleDetection()
    {
        // if no player exists in the scene, do nothing
        if (playerTarget == null) return;
        // calculate straight-line distance between this enemy and the player (playerTarget)
        float dist = Vector2.Distance(transform.position, playerTarget.position);
        // sets true if in attach range
        playerInRange = dist <= AttackRange;
    }

    // called every frame by EnemyBase.Update(), after HandleDetection to chase 
    // player if they're within detection range but outside attack range
    protected override void HandleMovement()
    {
        // Don't move if no player exists or already close enough to attack
        if (playerTarget == null || playerInRange) return;

        // calculate straight-line distance between this enemy and the player (playerTarget)
        float dist = Vector2.Distance(transform.position, playerTarget.position);

        // only chase if the player is within detection range
        // detectionRange is from the EnemyStatsSO asset
        if (dist <= stats.detectionRange)
        {
            // direction from enemy to player, normalized to length 1 so 
            // movement speed stays constant regardless of distance
            Vector2 direction = (playerTarget.position - transform.position).normalized;

            // move using physics so the enemy respects wall collisions
            // Time.deltaTime makes speed consistent regardless of frame rate
            rb.MovePosition(rb.position + direction * stats.moveSpeed * Time.deltaTime);
        }
    }

    // called by Update when the player is in attack range
    // parameter is IDamageable, not Player so works on anything that can take damage
    public override void Attack(IDamageable target)
    {
        // checks Cooldown: Time.time is seconds since the game started
        // if not enough time has passed since the last attack, do nothing
        if (Time.time - lastAttackTime < AttackCooldown) return;

        // calls TakeDamage on whatever was passed in (through interface)
        // AttackDamage comes from the EnemyStatsSO via EnemyBase
        target.TakeDamage(AttackDamage);

        // record when this attack happened so the cooldown starts
        lastAttackTime = Time.time;
    }

    // runs every frame and overrides EnemyBase.Update to add attack logic
    protected override void Update()
    {
        // Call EnemyBase.Update() to run: isAlive check, HandleDetection(),HandleMovement()
        base.Update();

        // after detection and movement have run, check if we should attack
        // playerInRange set by HandleDetection inside base.Update()
        if (playerInRange && playerTarget != null)
        {
            // get the player's IDamageable interface
            // this is how player is passed to Attack() without knowing the concrete Player class
            IDamageable target = playerTarget.GetComponent<IDamageable>();

            // if the player implements IDamageable: attack 
            // If not do nothing
            if (target != null)
                Attack(target);
        }
    }
}