using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNode : MonoBehaviour
{
    [SerializeField] private int levelNumber = 1;
    [SerializeField] private string sceneToLoad;
    
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private GameObject checkmarkIcon;

    private void Start()
    {
        RefreshNode();
    }

    private void OnEnable()
    {
        RefreshNode();
    }

    public void RefreshNode()
    {
        if (ProgressionSystem.Instance == null)
        {
            Debug.LogWarning("No ProgressionSystem found for " + gameObject.name);
            return;
        }

        bool isUnlocked = ProgressionSystem.Instance.IsLevelUnlocked(levelNumber);
        bool isCompleted = levelNumber < ProgressionSystem.Instance.currentLevel;

        Debug.Log(gameObject.name + " | levelNumber = " + levelNumber +
                  " | currentLevel = " + ProgressionSystem.Instance.currentLevel +
                  " | isUnlocked = " + isUnlocked +
                  " | isCompleted = " + isCompleted);

        if (lockIcon != null)
            lockIcon.SetActive(!isUnlocked);

        if (checkmarkIcon != null)
            checkmarkIcon.SetActive(isCompleted);
    }

    private void OnMouseDown()
    {
        if (ProgressionSystem.Instance == null)
        {
            Debug.LogWarning("No ProgressionSystem found when clicking " + gameObject.name);
            return;
        }

        bool isUnlocked = ProgressionSystem.Instance.IsLevelUnlocked(levelNumber);

        Debug.Log("Clicked " + gameObject.name +
                  " | levelNumber = " + levelNumber +
                  " | currentLevel = " + ProgressionSystem.Instance.currentLevel +
                  " | isUnlocked = " + isUnlocked);

        if (!isUnlocked)
        {
            Debug.Log("Level " + levelNumber + " is locked.");
            return;
        }

        if (string.IsNullOrWhiteSpace(sceneToLoad))
        {
            Debug.LogWarning("Scene name is empty on " + gameObject.name);
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}