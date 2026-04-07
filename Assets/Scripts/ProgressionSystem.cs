using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    
    public static ProgressionSystem Instance { get; private set; }
    
    //Variables 
    public int coins = 0;
    public int currentLevel = 1;
    public int ingredientsCollected = 0;
    public int totalIngredients = 5;
    private static ProgressionSystem instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    
    
    public void AddCoins(int amount)
    {
        
        coins += amount;
        Debug.Log("Coins: " + coins);
    }
    
    public void CollectIngredient()
    {
        ingredientsCollected++;
        Debug.Log("Ingredients Collected: " + ingredientsCollected);
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
        
        //Print in the console 
        Debug.Log("Level completed, Currently at level " + currentLevel);
        currentLevel++;
    }
}
