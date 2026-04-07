using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    
    public static ProgressionSystem Instance { get; private set; }
    
    //Variables 
    public int coins = 0;
    public int currentLevel = 1;
    public int ingredientsCollected = 0;
    public int totalIngredients = 5;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keeps it alive between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AddCoins(int amount)
    {
        coins += amount;
    }
    
    public void CollectIngredient()
    {
        ingredientsCollected++;
    }

    public bool AllIngredientsCollected()
    {
        return ingredientsCollected >= totalIngredients;
    }
    
    public bool IsLevelUnlocked(int levelNumber)
    {
        return levelNumber <= currentLevel;
    }
    
    public void CompleteLevel()
    {
        currentLevel++;
        
        //Print in the console 
        Debug.Log("Level completed, Currently at level " + currentLevel);
    }
}
