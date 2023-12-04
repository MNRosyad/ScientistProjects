using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // Besar gaya dorongan

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Pastikan hanya pemain yang bisa menggunakan jump pad
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // Berikan dorongan pada pemain
                Vector2 jumpVelocity = new Vector2(playerRb.velocity.x, jumpForce);
                playerRb.velocity = jumpVelocity;
            }
        }
    }
}
