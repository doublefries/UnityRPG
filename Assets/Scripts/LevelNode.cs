using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNode : MonoBehaviour
{
    [SerializeField] private int levelNumber = 1;
    [SerializeField] private string sceneToLoad;

    private bool isUnlocked;

    private void Start()
    {
        UpdateUnlockedState();
    }

    public void UpdateUnlockedState()
    {
        if (ProgressionSystem.Instance == null)
        {
            Debug.LogWarning("No ProgressionSystem found in scene.");
            return;
        }

        isUnlocked = ProgressionSystem.Instance.IsLevelUnlocked(levelNumber);

        // Optional: visually show locked/unlocked here
        Debug.Log("Level " + levelNumber + " unlocked: " + isUnlocked);
    }

    private void OnMouseDown()
    {
        if (!isUnlocked)
        {
            Debug.Log("Level " + levelNumber + " is locked.");
            return;
        }

        Debug.Log("Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}