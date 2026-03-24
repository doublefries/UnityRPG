using UnityEngine;
using System;

public interface IDamageable
{
    // read-only health
    int CurrentHealth { get; }
    // read-only health cap
    int MaxHealth { get; }
    // takes int for how much damage to deal
    void TakeDamage(int amount);
    // handles deaths
    void Die();
    // update health value when changes
    event Action<int> OnHealthChanged;
    // action on die, maybe contibutes to coins
    event Action OnDeath;
}
