using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    //Variables 
    public int coins = 0;
    public int currentLevel = 1;

    public void CompleteLevel()
    {
        currentLevel += 1;
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
