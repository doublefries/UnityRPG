using UnityEngine;

// Can be attached to any game Object in scene 
public class DamageDealer : MonoBehaviour
{
    // [SerializeField] tells unity to show this private field in the inspector
    // can edit quickly 
    // variable to store the strength of damage 
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private bool destroyOnHit = false;
    [SerializeField] private bool isPlayerAttack = false;

    private int EffectiveDamage
    {
        get
        {
            if (!isPlayerAttack || ProgressionSystem.Instance == null)
                return damageAmount;
            return damageAmount + ProgressionSystem.Instance.strengthBonus;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable == null)
            damageable = other.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(EffectiveDamage);

            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}