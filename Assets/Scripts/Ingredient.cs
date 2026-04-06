using UnityEngine;
using UnityEngine.SceneManagement;

public class Ingredient : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         ProgressionSystem ps = FindObjectOfType<ProgressionSystem>();

         if (ps != null)
         {
            ps.CollectIngredient();
         }
         
         Debug.Log("Ingredient collected");
         Destroy(gameObject);
         ps.CompleteLevel();

         SceneManager.LoadScene("IsometricScene");
      }
   }
}
