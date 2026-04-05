using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneClickTarget : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void LoadAssignedScene()
    {
        Debug.Log("Clicked on: " + gameObject.name);

        if (string.IsNullOrWhiteSpace(sceneToLoad))
        {
            Debug.LogWarning("Scene name is empty on " + gameObject.name);
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnMouseDown()
    {
        LoadAssignedScene();
    }
}