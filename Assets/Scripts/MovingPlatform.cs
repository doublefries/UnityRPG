using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private bool startMovingRight = true;

    private Vector3 _startPosition;
    private float _horizontalOffset;
    private int _direction;

    private void Awake()
    {
        _startPosition = transform.position;
        _direction = startMovingRight ? 1 : -1;
    }

    private void Update()
    {
        if (moveDistance <= 0f || moveSpeed <= 0f)
        {
            return;
        }

        _horizontalOffset += _direction * moveSpeed * Time.deltaTime;

        if (_horizontalOffset > moveDistance)
        {
            _horizontalOffset = moveDistance;
            _direction = -1;
        }
        else if (_horizontalOffset < -moveDistance)
        {
            _horizontalOffset = -moveDistance;
            _direction = 1;
        }

        transform.position = _startPosition + Vector3.right * _horizontalOffset;
    }

    private void OnValidate()
    {
        moveDistance = Mathf.Max(0f, moveDistance);
        moveSpeed = Mathf.Max(0f, moveSpeed);
    }
}
