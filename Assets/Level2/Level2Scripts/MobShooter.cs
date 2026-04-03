using UnityEngine;

public class MobShooter : MonoBehaviour
{
    //Variables 
    public float rightX = 4f;
    public float leftX = -4f;
    public float speed = 2f;
    
    public bool movingRight = true;

    // Update is called once per frame
    void Update()
    {
        //Moving right
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            
            //This is checking if the bottom limit is reached 
            if (transform.position.x >= rightX)
            {
                movingRight = false;
            }
        }
        
        //This is left
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            //Checking  the top limit
            if (transform.position.x <= leftX)
            {
                movingRight = true;
            }
        }
    }
    
}