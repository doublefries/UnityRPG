using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _movement;
    private bool _isAttacking = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !_isAttacking)
        {
            _isAttacking = true;
        }
    }
}
