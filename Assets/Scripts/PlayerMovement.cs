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
    Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _jumpQueued;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>(); //list for the collisions that the ray cast finds
    
    Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>(); 
    }

    // Capture input in Update for better responsiveness
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            _jumpQueued = true;
        }
        
        UpdateAnimator();
    }
    
    // Physics updates in FixedUpdate
    void FixedUpdate()
    {
        //If movement input is not 0 player will try to move
        if (_moveInput != Vector2.zero)
        {
           bool success = TryMove(_moveInput);
           
           //Makes movement along collisions smoother
           if (!success)
           {
               success = TryMove(new Vector2(_moveInput.x,0));

               if (!success)
               {
                   success = TryMove(new Vector2(0, _moveInput.y));
               }
           }
        }

        if (_jumpQueued)
        {
            Jump();
            _jumpQueued = false;
        }
    }

    private bool TryMove(Vector2 direction) //Better with handling collisions
    {
        //If we get count of 0, there are no collision, move is valid
        int count = _rb.Cast(direction, movementFilter, _castCollisions, currentMoveSpeed *Time.fixedDeltaTime + collisionOffset);
        Console.Write(count);
        if (count == 0)
        {
            _rb.MovePosition(_rb.position + (direction * currentMoveSpeed * Time.fixedDeltaTime));
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
        if (groundCheckPoint == null)
        {
            return false;
        }

        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayerMask);
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
