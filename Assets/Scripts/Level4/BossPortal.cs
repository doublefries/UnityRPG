using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPortal : MonoBehaviour
{
    [SerializeField] private string nextScene = "isometricScene";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Portal entered — loading " + nextScene);
        SceneManager.LoadScene(nextScene);
    }
}