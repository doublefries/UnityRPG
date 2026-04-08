using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null)
        {
            slider.maxValue = playerHealth.MaxHealth;
            slider.value = playerHealth.CurrentHealth;
            playerHealth.OnHealthChanged += UpdateHealthBar;
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHealth)
    {
        slider.value = currentHealth;
    }
}