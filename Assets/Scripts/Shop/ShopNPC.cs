using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            shopManager.OpenShop();
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.Escape))
        {
            shopManager.CloseShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to open shop.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            shopManager.CloseShop();
        }
    }
}