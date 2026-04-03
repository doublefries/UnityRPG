using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    //The initial health 
    public int health = 3;
    private bool isDead = false;
    
    //The 3 heart objects 
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    //TO run when game starts 
    void Start()
    {
        UpdateHearts();
    }



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
        
        UpdateHearts();
    }
    
    //Shows what heats are visible 
    private void UpdateHearts()
    {
        heart1.SetActive(health >= 1);
        heart2.SetActive(health >= 2);
        heart3.SetActive(health >= 3);
    }
}