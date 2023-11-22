using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabController : MonoBehaviour
{
    [SerializeField]
    private Transform grabDetect;
    [SerializeField]
    private Transform boxHolder;
    [SerializeField]
    private float rayDistance;

    private bool grabToggle;
    private Collider2D savedCollider;
    private CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.left * transform.localScale, rayDistance);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            grabToggle = !grabToggle;
            Collider2D detectBox = grabCheck.collider;
            if (grabToggle == true)
            {
                if (detectBox != null && detectBox.tag == "Box")
                {
                    detectBox.transform.parent = boxHolder;
                    detectBox.transform.position = boxHolder.position;
                    detectBox.GetComponent<Rigidbody2D>().isKinematic = true;
                    detectBox.enabled = false;

                    capsuleCollider.direction = CapsuleDirection2D.Horizontal;
                    capsuleCollider.size = new Vector2(2.3f, 2f);
                    capsuleCollider.offset = new Vector2(-0.7f, -0.4f);

                    grabCheck.rigidbody.velocity = Vector2.zero;
                    savedCollider = detectBox;
                }
            }
            else if (grabToggle == false)
            {
                savedCollider.transform.parent = null;
                savedCollider.GetComponent<Rigidbody2D>().isKinematic = false;
                savedCollider.enabled = true;

                capsuleCollider.direction = CapsuleDirection2D.Vertical;
                capsuleCollider.size = new Vector2(0.8f, 2f);
                capsuleCollider.offset = new Vector2(-0.02f, -0.4f);
            }
        }
    }
}
