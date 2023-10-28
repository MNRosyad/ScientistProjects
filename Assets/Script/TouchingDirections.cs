using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D touchingCol;
    Animator animator;

    public float groundDistance = 0.05f;

    public ContactFilter2D castFilter;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded { get
        {
            return _isGrounded;
        } private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationString.isGrounded, value);
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
