using System;
using System.Collections;
using UnityEngine;

public class Level4PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float flickerInterval = 0.1f;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    private bool isAlive = true;
    private bool isInvincible = false;
    private Vector3 spawnPoint;
    private SpriteRenderer[] spriteRenderers;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        spawnPoint = transform.position;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    public void TakeDamage(int amount)
    {
        if (!isAlive || isInvincible) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        OnHealthChanged?.Invoke(CurrentHealth);

        Debug.Log("Player health: " + CurrentHealth + "/" + MaxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityFlash());
    }

    public void Die()
    {
        if (!isAlive) return;

        isAlive = false;
        Debug.Log("Player died!");
        OnDeath?.Invoke();

        Respawn();
    }

    private void Respawn()
    {
        transform.position = spawnPoint;
        CurrentHealth = maxHealth;
        isAlive = true;
        isInvincible = false;

        SetSpritesVisible(true);
        OnHealthChanged?.Invoke(CurrentHealth);
    }

    private IEnumerator InvincibilityFlash()
    {
        isInvincible = true;

        float elapsed = 0f;
        bool visible = false;

        while (elapsed < invincibilityDuration)
        {
            visible = !visible;
            SetSpritesVisible(visible);

            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval;
        }

        SetSpritesVisible(true);
        isInvincible = false;
    }

    private void SetSpritesVisible(bool visible)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (sr != null)
                sr.enabled = visible;
        }
    }
}