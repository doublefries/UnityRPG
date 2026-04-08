using System;
using UnityEngine;

public class Level4PlayerHealth : MonoBehaviour, IDamageable
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

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        OnHealthChanged?.Invoke(CurrentHealth);
        Debug.Log("Player health: " + CurrentHealth + "/" + MaxHealth);
        if (CurrentHealth <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("Player died!");
        OnDeath?.Invoke();
    }
}