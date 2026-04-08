using UnityEngine;

public class IceTrap : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        IDamageable target = other.GetComponent<IDamageable>();
        if (target == null) target = other.GetComponentInParent<IDamageable>();

        if (target != null)
        {
            target.TakeDamage(damage);
            Debug.Log("Trap hit player for " + damage + " damage");
        }

        Destroy(gameObject);
    }
}