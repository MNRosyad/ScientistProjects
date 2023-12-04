using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    CapsuleCollider2D playerCapsuleCollider;

    public float walkSpeed = 7f;
    public float crouchSpeed = 4f;
    public float jumpPower = 19f;
    private bool isMovementEnabled = true;

    public float CurrentMove
    {
        get
        {
            if(IsMoving && !touchingDirections.IsOnWall)
            {
                return walkSpeed;
            }
            else
            {
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationString.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isFacingRight = false;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
            }

            _isFacingRight = value;
        }
    }

    [SerializeField]
    private bool _isCrouching = false;

    public bool IsCrouching 
    { 
        get
        {
            return _isCrouching;
        } 
        private set
        {
            _isCrouching = value;
        }
    }

    [SerializeField]
    private bool _passPlatform = false;

    public bool PassPlatform
    {
        get
        {
            return _passPlatform;
        }
        private set
        {
            _passPlatform = value;
        }
    }

    [SerializeField]
    private bool _interaction = false;

    public bool Interaction
    {
        get
        {
            return _interaction;
        }
        private set
        {
            _interaction = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        if (isMovementEnabled)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMove, rb.velocity.y);
            animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isMovementEnabled) 
        {
            moveInput = context.ReadValue<Vector2>();
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // left
            IsFacingRight = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            IsCrouching = true;
        }
        else if (context.canceled)
        {
            IsCrouching = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsCrouching == false)
        {
            if (context.started && touchingDirections.IsGrounded)
            {
                //animator.SetTrigger(AnimationString.jump);
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
        }
    }

    public void OnDownPlatform(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            PassPlatform = true;
        }
        else if (context.canceled)
        {
            PassPlatform = false;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Interaction = true;
        }
        else if(context.canceled)
        {
            Interaction = false;
        }
    }

    public void AdjustColliderSize(float width, float height)
    {
        playerCapsuleCollider.size = new Vector2(width, height);
    }

    public void AdjustColliderOffset(float xOffset, float yOffset)
    {
        playerCapsuleCollider.offset = new Vector2(xOffset, yOffset);
    }

    public void SetCapsuleDirection(CapsuleDirection2D direction)
    {
        playerCapsuleCollider.direction = direction;
    }
    public void DisableMovement()
    {
        isMovementEnabled = false;
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
    }
}
