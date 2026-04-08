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

            ProgressionSystem.Instance.CompleteLevel(2);
            SceneManager.LoadScene("IsometricScene");
        }
    }
}