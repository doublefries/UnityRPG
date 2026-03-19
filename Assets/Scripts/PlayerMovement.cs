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
    List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>(); //list for the collisions that the ray cast finds

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Capture input in Update for better responsiveness
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
    }
    
    // Physics updates in FixedUpdate
    void FixedUpdate()
    {
        //If movement input is not 0 player will try to move
        if (_moveInput != Vector2.zero)
        {
            //If we get count of 0, there are no collision, move is valid
            int count = _rb.Cast(_moveInput, movementFilter, _castCollisions, currentMoveSpeed *Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                _rb.MovePosition(_rb.position + (_moveInput * currentMoveSpeed * Time.fixedDeltaTime));
            }
        }
    }
    void OnMove(InputValue movementValue)
    {
        _moveInput = movementValue.Get<Vector2>();
    }

    
}
