using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneRestartButton : MonoBehaviour
{
    [SerializeField] private string targetSceneName = "IsometricScene";

    public void RestartGame()
    {
        if (ProgressionSystem.Instance != null)
        {
            ProgressionSystem.Instance.currentLevel = 1;
            ProgressionSystem.Instance.coins = 0;
            ProgressionSystem.Instance.ingredientsCollected = 0;
            ProgressionSystem.Instance.speedBoost = 0f;
            ProgressionSystem.Instance.strengthBoost = 0;

            Debug.Log("Progress reset. currentLevel = 1");
        }
        else
        {
            Debug.LogWarning("No ProgressionSystem instance found.");
        }

        SceneManager.LoadScene(targetSceneName);
    }
}