using UnityEngine;
using UnityEngine.SceneManagement; 

public class ShopScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Shop");
    }
}