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

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.left * transform.localScale, rayDistance);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            grabToggle = !grabToggle;
            Collider2D detectBox = grabCheck.collider;
            if (grabToggle == true)
            {
                if (grabCheck.collider != null && grabCheck.collider.tag == "Box")
                {
                    detectBox.transform.parent = boxHolder;
                    detectBox.transform.position = boxHolder.position;
                    detectBox.GetComponent<Rigidbody2D>().isKinematic = true;
                    detectBox.enabled = false;

                    grabCheck.rigidbody.velocity = Vector2.zero;
                    savedCollider = detectBox;
                }
            }
            else if (grabToggle == false)
            {
                savedCollider.transform.parent = null;
                savedCollider.GetComponent<Rigidbody2D>().isKinematic = false;
                savedCollider.enabled = true;
            }
        }
    }
}
