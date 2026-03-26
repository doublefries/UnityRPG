using System;
using UnityEngine;

// Temporary stand-in for the real Player class so enemies have something to chase and damage
// Implements IDamageable so the collision and enemy systems can interact with it
// Delete this once the real player controller is built
public class TestPlayer : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    private void Update()
    {
        // Simple WASD movement for testing — returns -1, 0, or 1 per axis
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector3(h, v, 0) * 5f * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        OnHealthChanged?.Invoke(CurrentHealth);
        // Prints to Unity Console so you can watch damage happening in real time
        Debug.Log($"Player took {amount} damage. Health: {CurrentHealth}/{MaxHealth}");

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log("Player died!");
        OnDeath?.Invoke();
    }
}