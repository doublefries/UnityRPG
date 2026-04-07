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
    Rigidbody2D _rb;
    private Vector2 _moveInput;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    
    Animator _animator;
    
    // Reference to ice physics — null on levels without ice
    private IcePhysicsController _iceController;
    
    public Vector2 MoveInput => _moveInput;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _iceController = GetComponent<IcePhysicsController>();
    }
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        
        UpdateAnimator();
    }
    
    void FixedUpdate()
    {
        // If on ice, IcePhysicsController handles movement — skip normal movement
        if (_iceController != null && _iceController.IsSliding) return;

        if (_moveInput != Vector2.zero)
        {
           bool success = TryMove(_moveInput);
           
           if (!success)
           {
               success = TryMove(new Vector2(_moveInput.x,0));
               if (!success)
               {
                   success = TryMove(new Vector2(0, _moveInput.y));
               }
           }
        }
    }
    private bool TryMove(Vector2 direction)
    {
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
}