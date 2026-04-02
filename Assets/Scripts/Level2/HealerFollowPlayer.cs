using UnityEngine;

public class HealerFollowPlayer : MonoBehaviour
{
    //store the position of the player
    public Transform player;
    
    //Speed and distance of healer
    public float speed = 4f;
    public float followDistance = 1.5f;

    void Update()
    {
            //Distance between player and healer 
            float distance = Vector2.Distance(transform.position, player.position);

            //When the healer is far away 
            if (distance > followDistance)
            {
                //Moves the healer 
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
    }
}