using UnityEngine;

public class MobRunner : MonoBehaviour
{
    //Variables 
    public float topY = 4f;
    public float bottomY = -4f;
    public float speed = 2f;
    public float PosX = 3;
    
    public bool movingDown = true;

    // Update is called once per frame
    void Update()
    {
        //Moving down
        if (movingDown)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            
            //This is checking if the bottom limit is reached 
            if (transform.position.y <= bottomY)
            {
                movingDown = false;
            }
        }
        
        //This is moving up 
        else
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            //Checking  the top limit
            if (transform.position.y >= topY)
            {
                movingDown = true;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Once hit, moves back up again so the cycle can continue again 
            transform.position = new Vector3(PosX, 10f, 0f);
            movingDown = true;
        }
        
        if (collision.CompareTag("Healer"))
        {
            //Once hit, moves back up again so the cycle can continue again 
            transform.position = new Vector3(PosX, 10f, 0f);
            movingDown = true;
        }
    }
    
}
