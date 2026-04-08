using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  
  [SerializeField] private string isometricSceneName = "IsometricScene";
  
  public Character selectedCharacter {get; private set;}

  private void Awake()
  {
    if (instance != null)
    {
      Destroy(gameObject);
      return;
    }

    instance = this;
    DontDestroyOnLoad(gameObject); //Continue for when we swap scenes
  }

  public void SelectCharacter(Character character)
  {
    selectedCharacter = character;
    Debug.Log("Selected: " + character.name);
  }

  public void StartGame()
  {
    if (selectedCharacter == null)
    {
      Debug.LogWarning("No character selected");
      return;
    }

    SceneManager.LoadScene(isometricSceneName);
  }
}
