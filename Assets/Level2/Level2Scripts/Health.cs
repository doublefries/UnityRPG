using UnityEngine;

public class Health : MonoBehaviour
{
    //The initial health 
    public int health = 3;

    //Method to take damage
    public void TakeDamage(int amount)
    {
        health -= amount;
        
        //to see current health 
        Debug.Log(gameObject.name + " Health: " + health);

        if (health <= 0)
        {
            //failed level 
            Debug.Log(gameObject.name + " Died, Level failed ");
        }
    }
}