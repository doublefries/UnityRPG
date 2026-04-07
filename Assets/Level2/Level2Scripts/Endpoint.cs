using UnityEngine;
using UnityEngine.SceneManagement; 

public class Endpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Healer"))
        {
            FindObjectOfType<ProgressionSystem>().CompleteLevel(2);
            FindObjectOfType<ProgressionSystem>().CollectIngredient();

            SceneManager.LoadScene("IsometricScene");
        }
    }
}