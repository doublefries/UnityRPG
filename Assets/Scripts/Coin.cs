using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ProgressionSystem>().AddCoins(1);
            Destroy(gameObject);
        }
    }
}