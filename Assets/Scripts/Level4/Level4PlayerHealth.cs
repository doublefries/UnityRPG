using System;
using UnityEngine;

public class Level4PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    private bool isAlive = true;

    private Vector3 spawnPoint;

    private void Awake()
    {
        CurrentHealth = maxHealth;

        // save starting position
        spawnPoint = transform.position;
    }

    public void TakeDamage(int amount)
    {
        if (!isAlive) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        OnHealthChanged?.Invoke(CurrentHealth);

        Debug.Log("Player health: " + CurrentHealth + "/" + MaxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (!isAlive) return;

        isAlive = false;
        Debug.Log("Player died!");

        OnDeath?.Invoke();

        // Respawn
        Respawn();
    }

    private void Respawn()
    {
        Debug.Log("Respawning player...");

        transform.position = spawnPoint;

        CurrentHealth = maxHealth;
        isAlive = true;

        OnHealthChanged?.Invoke(CurrentHealth);
    }
}