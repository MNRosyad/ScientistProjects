using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb; 
   public float speed;
   public float jumpforce;
   public LayerMask ground;

    void Start()
    {
        rb= GetComponent <Rigidbody2D>();
    }

   
    void Update()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horiz * speed, rb.velocity.y); 

        if (horiz > 0 || horiz < 0)
        {
            transform.localScale *= new Vector2(-1,1);
        }

        if (Input.GetKeyUp (KeyCode.Space))
        {
            if (!IsGrounded())
            {
                return;
            }
            else
            {
                rb.velocity = Vector2.up * jumpforce;
            }
            
        } 
    }

    bool IsGrounded()
    {
        Vector2 position= transform.position;
        Vector2 direction = Vector2.down;
        float disctance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position,direction,disctance,ground);
        if(hit.collider !=null)
        {
            return true;
        }
        return false;

    }
}
