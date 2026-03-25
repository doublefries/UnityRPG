using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  
  public Character[] characters; //Contains player and player blue

  public Character currentCharacter;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this; //Create manager
    }

    else
    {
      Destroy(gameObject); //If we already have a game manager, destroy the one we created
    }
    
    DontDestroyOnLoad(gameObject); //Continue for when we swap scenes
  }
}
