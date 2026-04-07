using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.3f;
    [SerializeField] private float hitboxActiveTime = 0.12f;

    [SerializeField] private GameObject hitboxUp;
    [SerializeField] private GameObject hitboxDown;
    [SerializeField] private GameObject hitboxLeft;
    [SerializeField] private GameObject hitboxRight;

    private Animator _animator;
    private PlayerMovement _movement;
    private bool _isAttacking = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();

        DisableAllHitboxes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !_isAttacking)
        {
            _isAttacking = true;

            Vector2 moveInput = _movement.MoveInput;

            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            {
                _animator.SetFloat("MoveX", moveInput.x > 0 ? 1 : -1);
                _animator.SetFloat("MoveY", 0);
            }
            else if (moveInput != Vector2.zero)
            {
                _animator.SetFloat("MoveX", 0);
                _animator.SetFloat("MoveY", moveInput.y > 0 ? 1 : -1);
            }

            _animator.SetTrigger("Attack");
            StartCoroutine(DoAttack());
        }
    }

    private IEnumerator DoAttack()
    {
        GameObject activeHitbox = GetCurrentHitbox();

        if (activeHitbox != null)
            activeHitbox.SetActive(true);

        yield return new WaitForSeconds(hitboxActiveTime);

        if (activeHitbox != null)
            activeHitbox.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);

        _isAttacking = false;
    }

    private GameObject GetCurrentHitbox()
    {
        float moveX = _animator.GetFloat("MoveX");
        float moveY = _animator.GetFloat("MoveY");

        if (moveX > 0) return hitboxRight;
        if (moveX < 0) return hitboxLeft;
        if (moveY > 0) return hitboxUp;
        return hitboxDown;
    }

    private void DisableAllHitboxes()
    {
        if (hitboxUp != null) hitboxUp.SetActive(false);
        if (hitboxDown != null) hitboxDown.SetActive(false);
        if (hitboxLeft != null) hitboxLeft.SetActive(false);
        if (hitboxRight != null) hitboxRight.SetActive(false);
    }
}