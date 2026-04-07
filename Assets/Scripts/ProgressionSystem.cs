using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    public static ProgressionSystem Instance { get; private set; }
    
    public int coins = 0;
    public int currentLevel = 1;
    public int ingredientsCollected = 0;
    public int totalIngredients = 5;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("ProgressionSystem Awake. currentLevel = " + currentLevel);
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
        Debug.Log("Checking unlock: levelNumber = " + levelNumber + ", currentLevel = " + currentLevel);
        return levelNumber <= currentLevel;
    }

    public void CompleteLevel()
    {
        Debug.Log("Level completed, Currently at level " + currentLevel);
        currentLevel++;
    }
}