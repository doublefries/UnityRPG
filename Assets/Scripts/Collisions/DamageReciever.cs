using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    // stores a reference to whatever is implementing IDamageable
    private IDamageable damageable;

    // runs automatically when object is initalized
    private void Awake()
    {
        // searches for component that implements IDamageable on object or parent  
        damageable = GetComponentInParent<IDamageable>();
        // if no IDamageable exists logs a error to unity console
        if (damageable == null)
            Debug.LogError($"No IDamageable found on {gameObject.name} or its parents.");
    }

    // allows damage to be dealt to the object 
    public void ReceiveDamage(int amount)
    {
        damageable?.TakeDamage(amount);
    }
}
