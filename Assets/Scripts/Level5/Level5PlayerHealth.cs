using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private Level1UIManager uiManager;
    [SerializeField] private FallingTiles fallingTiles;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    private bool isDead = false;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth);

        if (uiManager == null)
            uiManager = FindObjectOfType<Level1UIManager>();

        if (fallingTiles == null)
            fallingTiles = FindObjectOfType<FallingTiles>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        OnDeath?.Invoke();

        if (fallingTiles != null)
            fallingTiles.StopFalling();

        if (uiManager != null)
            uiManager.PlayerDied();
        else
            Debug.LogWarning("No Level1UIManager found.");
    }
}