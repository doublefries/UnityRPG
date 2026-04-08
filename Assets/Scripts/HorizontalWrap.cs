using UnityEngine;

public class HorizontalWrap : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform leftWrapPoint;
    [SerializeField] private Transform rightWrapPoint;
    [SerializeField] private float wrapOffset = 0.2f;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = rb.position;

        if (pos.x > rightWrapPoint.position.x)
        {
            rb.position = new Vector2(leftWrapPoint.position.x + wrapOffset, pos.y);
        }
        else if (pos.x < leftWrapPoint.position.x)
        {
            rb.position = new Vector2(rightWrapPoint.position.x - wrapOffset, pos.y);
        }
    }
}
