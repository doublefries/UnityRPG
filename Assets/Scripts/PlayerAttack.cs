using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.3f;
    private Animator _animator;
    private PlayerMovement _movement;
    private bool _isAttacking = false; //flag to prevent from attack spam

    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !_isAttacking) //player has pressed f and is not already mid-attack
        {
            _isAttacking = true;
            
            Vector2 moveInput = _movement.MoveInput; //read the current movement direction from player movement via the public property

            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            {
                _animator.SetFloat("MoveX", moveInput.x > 0 ? 1 : -1);
                _animator.SetFloat("MoveY", 0);
            }
            else if (moveInput != Vector2.zero) //if player is standing still animator keeps last set direction
            {
                _animator.SetFloat("MoveX", 0);
                _animator.SetFloat("MoveY", moveInput.y >  0 ? 1 : -1);
            }
            
            _animator.SetTrigger("Attack");
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        _isAttacking = false;
    }
}
