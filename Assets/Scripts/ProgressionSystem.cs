using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
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
        Debug.Log("Coins: " + coins);
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
    
    public void CompleteLevel()
    {
        currentLevel++;
        
        //Print in the console 
        Debug.Log("Level completed, Currently at level " + currentLevel);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}