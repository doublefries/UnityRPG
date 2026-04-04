using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ProgressionSystem>().AddCoins(1);
            Destroy(gameObject);
        }
    }
}