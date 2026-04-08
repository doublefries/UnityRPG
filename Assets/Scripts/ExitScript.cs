using UnityEngine;
using UnityEngine.SceneManagement; 

public class ExitScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("IsometricScene");
    }
}
