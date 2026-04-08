using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  
  public Character[] characters; //Contains player and player blue

  public Character currentCharacter;
  
  [SerializeField] private string isometricSceneName = "isometricScene";

  private void Awake()
  {
    if (instance == null)
    {
      instance = this; //Create manager
    }

    else
    {
      Destroy(gameObject); //If we already have a game manager, destroy the one we created
      return;
    }
    
    DontDestroyOnLoad(gameObject); //Continue for when we swap scenes
  }

  private void Start()
  {
    if (characters.Length == 0 && currentCharacter == null)
    {
      currentCharacter = characters[0]; //Initial set
    }
  }
  
  public void SetCharacter(Character character)
  {
      currentCharacter = character;
  }

  public void StartGame()
  {
    if (currentCharacter == null)
    {
      Debug.LogWarning("No character selected");
      return;
    }

    SceneManager.LoadScene(isometricSceneName);
  }
}
