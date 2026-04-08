using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    [SerializeField] private BossEnemy boss;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject healthBarRoot;

    private void Start()
    {
        if (boss == null)
        {
            Debug.LogWarning("BossHealthBarUI: Boss reference is missing.");
            return;
        }

        if (slider == null)
        {
            Debug.LogWarning("BossHealthBarUI: Slider reference is missing.");
            return;
        }

        // start hidden until boss fight begins if desired
        if (healthBarRoot != null)
            healthBarRoot.SetActive(false);

        slider.minValue = 0;
        slider.maxValue = boss.MaxHealth;
        slider.value = boss.CurrentHealth;

        boss.OnHealthChanged += UpdateHealthBar;
        boss.OnDeath += HandleBossDeath;
    }

    private void OnDestroy()
    {
        if (boss != null)
        {
            boss.OnHealthChanged -= UpdateHealthBar;
            boss.OnDeath -= HandleBossDeath;
        }
    }

    public void ShowBar()
    {
        if (healthBarRoot != null)
            healthBarRoot.SetActive(true);

        if (boss != null && slider != null)
        {
            slider.maxValue = boss.MaxHealth;
            slider.value = boss.CurrentHealth;
        }
    }

    private void UpdateHealthBar(int currentHealth)
    {
        if (slider != null)
            slider.value = currentHealth;
    }

    private void HandleBossDeath()
    {
        if (healthBarRoot != null)
            healthBarRoot.SetActive(false);
    }
}