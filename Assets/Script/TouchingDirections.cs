using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D touchingCol;
    Animator animator;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.01f;
    public float ceilingDistance = 0.05f;

    public ContactFilter2D castFilter;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private Vector2 wallCheck => gameObject.transform.localScale.x > 0 ? Vector2.left : Vector2.right;

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

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall { get
        {
            return _isOnWall;
        } private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationString.isOnWall, value);
        } 
    }

    [SerializeField]
    private bool _isOnCeiling;
    public bool IsOnCeiling { get
        {
            return _isOnCeiling;
        } private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationString.isOnCeiling, value);
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
        IsOnWall = touchingCol.Cast(wallCheck, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
