using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    // We remove the manual assignment of moveSpeed and let the Player class set it
    public float currentMoveSpeed = 1f;
    public float collisionOffset = 0.05f;
    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayerMask;
    [Header("Scene Movement Overrides")]
    [SerializeField] private bool enableJump = false;
    [SerializeField] private bool allowVerticalMovement = true;
    Rigidbody2D _rb;
    private Collider2D _collider;
    private Vector2 _moveInput;
    private bool _jumpQueued;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    
    Animator _animator;

    public bool AllowVerticalMovement => allowVerticalMovement;
    public bool EnableJumps => enableJump;

    public void SetMovementOptions(bool allowVertical, bool allowJump)
    {
        allowVerticalMovement = allowVertical;
        enableJump = allowJump;
    }

    public Vector2 MoveInput => _moveInput;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>(); 
    }
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        ApplyMovementRestrictions();

        if (enableJump && Input.GetButtonDown("Jump"))
        {
            _jumpQueued = true;
        }
        
        UpdateAnimator();
    }
    
    private float EffectiveMoveSpeed
    {
        get
        {
            float bonus = ProgressionSystem.Instance != null ? ProgressionSystem.Instance.speedBonus : 0f;
            return currentMoveSpeed + bonus;
        }
    }

    void FixedUpdate()
    {
        float speed = EffectiveMoveSpeed;

        if (enableJump)
        {
            float targetVelocityX = _moveInput.x * speed;
            _rb.linearVelocity = new Vector2(targetVelocityX, _rb.linearVelocity.y);
        }
        else
        {
            _rb.linearVelocity = _moveInput * speed;
        }

        if (enableJump && _jumpQueued)
        {
            Jump();
            _jumpQueued = false;
        }
    }
    private bool TryMove(Vector2 direction)
    {
        float speed = EffectiveMoveSpeed;
        int count = _rb.Cast(direction, movementFilter, _castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
        Console.Write(count);
        if (count == 0)
        {
            _rb.MovePosition(_rb.position + (direction * speed * Time.fixedDeltaTime));
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnMove(InputValue movementValue)
    {
        _moveInput = movementValue.Get<Vector2>();
        ApplyMovementRestrictions();
    }

    private void ApplyMovementRestrictions()
    {
        if (!allowVerticalMovement)
        {
            _moveInput.y = 0f;
        }
    }
    void UpdateAnimator()
    {
        bool isMoving = _moveInput != Vector2.zero;
        _animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            Vector2 normalizedInput = _moveInput.normalized;
            _animator.SetFloat("MoveX", normalizedInput.x);
            _animator.SetFloat("MoveY", normalizedInput.y);
        }
    }

    public bool CheckGrounded()
    {
        bool groundedByCheckPoint = false;
        if (groundCheckPoint != null)
        {
            groundedByCheckPoint = Physics2D.OverlapCircle(
                groundCheckPoint.position,
                groundCheckRadius,
                groundLayerMask
            );
        }

        // Also allow jumping when the player collider is touching valid ground layers.
        // This helps with moving/floating platforms while still respecting the layer mask.
        bool groundedByCollider = _collider != null && _collider.IsTouchingLayers(groundLayerMask);
        return groundedByCheckPoint || groundedByCollider;
    }

    public void Jump()
    {
        if (!CheckGrounded())
        {
            Debug.Log("Not grounded");
            return;
        }

        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
