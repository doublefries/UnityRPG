using UnityEngine;

public class EdgeScrollCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float fixedX = 0f;
    [SerializeField] private float yOffset = 2f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 100f;
    [SerializeField] private float followSpeed = 5f;

    private float fixedZ;

    private void Start()
    {
        fixedZ = transform.position.z;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        float targetY = Mathf.Clamp(player.position.y + yOffset, minY, maxY);

        Vector3 targetPosition = new Vector3(fixedX, targetY, fixedZ);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }
}