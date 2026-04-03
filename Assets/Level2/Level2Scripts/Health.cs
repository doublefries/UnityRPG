using UnityEngine;

public class Health : MonoBehaviour
{
    //The initial health 
    public int health = 3;
    private bool isDead = false;
    

    //Method to take damage
    public void TakeDamage(int amount)
    {
        if (isDead) 
        {
            return;
        }
        health -= amount;
        
        //to see current health 
        Debug.Log(gameObject.name + " Health: " + health);

        if (health <= 0)
        {
            health = 0;
            Debug.Log(gameObject.name + " Died, Level failed ");
            isDead = true;
        }
    }
}