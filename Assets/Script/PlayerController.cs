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
    GrabController grabBox;

    public float walkSpeed = 7f;
    public float crouchSpeed = 4f;
    public float jumpPower = 19f;

    public float CurrentMove
    {
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall)
            {
                if (IsCrouching)
                {
                    return crouchSpeed;
                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _enableMove = true;
    public bool EnableMove
    {
        get
        {
            return _enableMove;
        }
        private set
        {
            _enableMove = value;
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
            animator.SetBool(AnimationString.isCrouching, value);
        }
    }

    private bool _buttonCrouch = false;

    private bool ButtonCrouch 
    { 
        get
        {
            return _buttonCrouch;
        } 
        set
        {
            _buttonCrouch = value;
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

    [SerializeField]
    private bool _restartScene = false;

    public bool RestartScene
    {
        get
        {
            return _restartScene;
        }
        private set
        {
            _restartScene = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        grabBox = GetComponent<GrabController>();
    }

    private void FixedUpdate()
    {
        if (EnableMove)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMove, rb.velocity.y);
            animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);

            if (IsCrouching)
            {
                if (!ButtonCrouch && !touchingDirections.IsOnCeiling)
                {
                    IsCrouching = false;
                    if (!grabBox.grabToggle)
                    {
                        AdjustColliderOffset(-0.02f, -0.4f);
                        AdjustColliderSize(0.8f, 2f);
                    }
                    else if (grabBox.grabToggle)
                    {
                        SetCapsuleDirection(CapsuleDirection2D.Horizontal);
                        AdjustColliderOffset(-0.7f, -0.4f);
                        AdjustColliderSize(2.3f, 2f);
                    }
                }
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (EnableMove) 
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
        if (EnableMove)
        {
            if (context.started && touchingDirections.IsGrounded)
            {
                IsCrouching = true;
                ButtonCrouch = true;

                if (!grabBox.grabToggle)
                {
                    AdjustColliderOffset(-0.02f, -0.7f);
                    AdjustColliderSize(0.8f, 1.4f);
                }
                else if (grabBox.grabToggle)
                {
                    SetCapsuleDirection(CapsuleDirection2D.Horizontal);
                    AdjustColliderOffset(-0.7f, -0.7f);
                    AdjustColliderSize(2.3f, 1.4f);
                }
            }
            else if (context.canceled)
            {
                ButtonCrouch = false;
                if (touchingDirections.IsOnCeiling)
                {
                    IsCrouching = true;
                }

                else if (!touchingDirections.IsOnCeiling)
                {
                    IsCrouching = false;

                    if (!grabBox.grabToggle)
                    {
                        AdjustColliderOffset(-0.02f, -0.4f);
                        AdjustColliderSize(0.8f, 2f);
                    }
                    else if (grabBox.grabToggle)
                    {
                        SetCapsuleDirection(CapsuleDirection2D.Horizontal);
                        AdjustColliderOffset(-0.7f, -0.4f);
                        AdjustColliderSize(2.3f, 2f);
                    }
                }
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsCrouching == false && EnableMove)
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
        if (EnableMove)
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
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (EnableMove)
        {
            if (context.started)
            {
                Interaction = true;
            }
            else if (context.canceled)
            {
                Interaction = false;
            }
        }
    }

    public void OnRestartScene(InputAction.CallbackContext context)
    {
        if (EnableMove)
        {
            if (context.started)
            {
                RestartScene = true;
            }
            else if (context.canceled)
            {
                RestartScene = false;
            }
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
    public void EnableMovement(bool value)
    {
        EnableMove = value;
    }
}
