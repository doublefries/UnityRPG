using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Variables 
    public Transform Mobshooter; //Where bullet starts 
    public float topY = 4f;
    public float bottomY = -4f;
    public float speed = 2f;
    public float PosX = 3;
    public float minSpeed = 10f;
    public float maxSpeed = 15f;
    
    public bool movingDown = true;
    
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }
    

    // Update is called once per frame
    void Update()
    {
        //Moving down
        transform.Translate(Vector2.down * speed * Time.deltaTime);
       
        if (transform.position.y < -6f)
        {
            transform.position = Mobshooter.position;
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(1);
            }
            
            //Once hit, moves back up again so the cycle can continue again 
            transform.position = new Vector3(PosX, 10f, 0f);
            movingDown = true;
        }
        
        if (collision.CompareTag("Healer"))
        {
            
            Health health = collision.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(1);
            }
            //Once hit, moves back up again so the cycle can continue again 
            transform.position = new Vector3(PosX, 10f, 0f);
            movingDown = true;
        }
    }
    
    
}