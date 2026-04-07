using UnityEngine;
using UnityEngine.SceneManagement;

// Place at the end of Level 4 with a trigger Collider2D
// When the player touches it, completes the level and loads the next scene
public class LevelEnd : MonoBehaviour
{
    // The name of the next scene to load — must match exactly what's in Build Settings
    [SerializeField] private string nextSceneName = "Level5";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Find the ProgressionSystem singleton
        ProgressionSystem progression = FindObjectOfType<ProgressionSystem>();
        if (progression != null)
        {
            progression.CollectIngredient();
            progression.CompleteLevel();
        }

        // Load next level
        SceneManager.LoadScene(nextSceneName);
    }
}