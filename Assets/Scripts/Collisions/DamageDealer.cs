using UnityEngine;

// Can be attached to any game Object in scene 
public class DamageDealer : MonoBehaviour
{
    // [SerializeField] tells unity to show this private field in the inspector
    // can edit quickly 
    // variable to store the strength of damage 
    [SerializeField] private int damageAmount = 1;
    // determines whether or not the object should destroy itself after dealing damage i.e a bullet 
    [SerializeField] private bool destroyOnHit = false;

    // unity event method
    // automatically calls the method when a objects 2D trigger collider touches another object 2D trigger collider 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // gets component from the collided object that uses IDamageable interface 
        IDamageable damageable = other.GetComponent<IDamageable>();
        // checks whether the search failed
        if (damageable == null)
            // if fails then checks the parent hierchary for the interface
            damageable = other.GetComponentInParent<IDamageable>();

        // runs if successfully found 
        if (damageable != null)
        {
            // tells the target to take damage 
            damageable.TakeDamage(damageAmount);

            // checks whether object should destroy itself i.e arrows 
            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}