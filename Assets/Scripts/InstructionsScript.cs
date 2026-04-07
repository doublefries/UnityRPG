using UnityEngine;
using UnityEngine.SceneManagement; 

public class InstructionsScript : MonoBehaviour
{
    private void OnMouseDown()
    {
            SceneManager.LoadScene("InstructionScene");
    }
}
