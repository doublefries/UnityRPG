using System;
using UnityEngine;

// Temporary player for testing ice physics — delete when real player exists
// Moves normally on regular ground, yields to IcePhysicsController on ice
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class IceTestPlayer : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float moveSpeed = 5f;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    private Rigidbody2D rb;
    private IcePhysicsController iceController;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        // Cache reference to ice controller — may or may not exist on this player
        iceController = GetComponent<IcePhysicsController>();
    }

    private void FixedUpdate()
    {
        // If on ice, IcePhysicsController handles movement — do nothing here
        // This is the handoff: two scripts cooperate through the IsSliding flag
        if (iceController != null && iceController.IsSliding) return;

        // Normal movement — instant direction changes, instant stop on key release
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(h, v).normalized;
        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        OnHealthChanged?.Invoke(CurrentHealth);
        Debug.Log($"Player took {amount} damage. Health: {CurrentHealth}/{MaxHealth}");
        if (CurrentHealth <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("Player died!");
        OnDeath?.Invoke();
    }
}