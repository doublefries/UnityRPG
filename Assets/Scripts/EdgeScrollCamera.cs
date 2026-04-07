using UnityEngine;

public class EdgeScrollCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    // dead zone margins
    [SerializeField] private float horizontalMargin = 2.5f;
    [SerializeField] private float verticalMargin = 1.5f;
    
    [SerializeField] private float smoothSpeed = 5f;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (player == null) return;
        
        float camX = transform.position.x;
        float camY = transform.position.y;

        float leftBound = camX - horizontalMargin;
        float rightBound = camX + horizontalMargin;
        float bottomBound = camY - verticalMargin;
        float topBound = camY + verticalMargin;

        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (player.position.x < leftBound)
        {
            targetX = player.position.x + horizontalMargin;
        }
        else if (player.position.x > rightBound)
        {
            targetX = player.position.x - horizontalMargin;
        }

        if (player.position.y < bottomBound)
        {
            targetY = player.position.y + verticalMargin;
        }
        else if (player.position.y > topBound)
        {
            targetY = player.position.y - verticalMargin;
        }

        targetPosition = new Vector3(targetX, targetY, transform.position.z);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}