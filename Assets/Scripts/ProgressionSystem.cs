using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    public static ProgressionSystem Instance { get; private set; }

    public int coins = 0;
    public int currentLevel = 1;
    public int ingredientsCollected = 0;
    public int totalIngredients = 5;

    public float speedBoost = 0f;
    public int strengthBoost = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Duplicate ProgressionSystem destroyed on " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("ProgressionSystem Awake on " + gameObject.name +
                  ". currentLevel = " + currentLevel);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);
    }

    public bool SpendCoins(int amount)
    {
        if (coins < amount)
            return false;

        coins -= amount;
        return true;
    }

    public void IncreaseSpeed(float amount)
    {
        speedBoost += amount;
        Debug.Log("Speed Boost is now: " + speedBoost);
    }

    public void IncreaseStrength(int amount)
    {
        strengthBoost += amount;
        Debug.Log("Strength Boost is now: " + strengthBoost);
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

    public void CompleteLevel(int completedLevelNumber)
    {
        Debug.Log("CompleteLevel called with level " + completedLevelNumber + ". Before update, currentLevel = " + currentLevel);
        if (completedLevelNumber >= currentLevel)
        {
            currentLevel = completedLevelNumber + 1;
            Debug.Log("Level " + completedLevelNumber + " completed. currentLevel is now " + currentLevel);
        }
        else{
            Debug.Log("Level " + completedLevelNumber + " was already completed. currentLevel remains " + currentLevel);
        }
    }
}