using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGameButton : MonoBehaviour
{
    [SerializeField] private string targetSceneName = "IsometricScene";

    public void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
