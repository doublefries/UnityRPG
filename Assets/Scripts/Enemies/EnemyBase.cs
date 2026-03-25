using System;
using UnityEngine;

// attributes to automatically add a Collider2D and Rigidbody2D if they don't already exist
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

// EnemyBase inherets from MonoBehaviour, IAttackable, and IDamageable to have 
// lifecycle methods, damage methods, and attach methods

// IDamageable: connects to collision system so DamageDealer can hurt this enemy
// IAttackable: standardizes attack properties so all enemies deal damage the same way
public abstract class EnemyBase : MonoBehaviour, IDamageable, IAttackable
{
    // visible to inspector and not accessable outside inheritance 
    // asset that holds this enemy's numbers (health, speed, damage, etc
    [SerializeField] protected EnemyStatsSO stats;

    // IDamageable implementation

    // current health of the enemy: can be read by anyone but only set by inheritance
    public int CurrentHealth { get; protected set; }
    // reads max health from scriptable object 
    public int MaxHealth => stats.maxHealth;
    // updates change in health
    public event Action<int> OnHealthChanged;
    // when enemy dies the spawner removes it from list 
    public event Action OnDeath;

    // IAttackable

    // how much damage this enemy deals per hit
    public int AttackDamage => stats.attackDamage;
    // how close the player must be before this enemy can attack
    public float AttackRange => stats.attackRange;
    // seconds between attacks of this enemy
    public float AttackCooldown => stats.attackCooldown;

    // reference to the player's transform (position, rotation)
    // subclasses use this to calculate distance and direction for chasing/attacking
    protected Transform playerTarget;

    // reference to this enemy's Rigidbody2D
    // subclasses use this for physics-based movement (rb.MovePosition)
    protected Rigidbody2D rb;

    // flag to prevent dead enemies from doing anything
    // without this, an enemy could take damage after dying, or chase the player before its removed
    protected bool isAlive = true;



    // awake runs once when  GameObject created: base.Awake() first to keep this logic, then add their own
    protected virtual void Awake()
    {
        // set health to max so enemies spawn alive not at 0
        CurrentHealth = MaxHealth;
        // cache the Rigidbody2D reference for subclasses to use in movement
        // requireComponent guarantees this component exists
        rb = GetComponent<Rigidbody2D>();
    }

    // start runs once after Awake to initalize player
    protected virtual void Start()
    {
        // search scene for GameObject tagged "Player" (must agree on this tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // null check to prevent crash if no player exists
        if (player != null)
            playerTarget = player.transform;
    }

    // called when this enemy is hit, this is what DamageDealer triggers
    public virtual void TakeDamage(int amount)
    {
        // dead enemies ignore all damage so prevents dieing twice
        if (!isAlive) return;

        // subtract damage but clamp at zero so health can't be negative
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        // notify subs (maybe UI) that health changed
        // ?. : if nobody is listening do nothing instead of crashing
        OnHealthChanged?.Invoke(CurrentHealth);

        // If health hit zero call die
        if (CurrentHealth <= 0)
            Die();
    }


    // called when health reaches zero
    public virtual void Die()
    {
        // guard against Die running twice
        // two damage sources on the same frame would both call Die without this
        if (!isAlive) return;

        // set immediately so Update, TakeDamage, and everything else stops
        isAlive = false;

        // Notify subs: EnemySpawner removes this enemy from active list
        OnDeath?.Invoke();

        // Remove this enemy attached to gameObject from the scene 
        Destroy(gameObject);
    }

    // how this enemy deals damage: melee swings, ranged shoots a projectile, etc.
    public abstract void Attack(IDamageable target);
    // how this enemy moves: chase player, patrol a path, stand still, etc.
    protected abstract void HandleMovement();
    // how this enemy detects the player: distance check, line of sight, always aware, etc.
    protected abstract void HandleDetection();

    // runs every frame: Template Method pattern
    // base class defines the order: detect first, then move
    // subclasses fill in what detection and movement actually do
    protected virtual void Update()
    {
        // dead enemies don't run behavior loop
        if (!isAlive) return;

        // detection runs first: sets flags i.e "player is in range"
        HandleDetection();
        // movement runs second: reads those flags to decide what to do
        HandleMovement();
    }
}
