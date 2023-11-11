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

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.left * transform.localScale, rayDistance);

        if (grabCheck.collider != null && grabCheck.collider.tag == "Box")
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                grabToggle = !grabToggle;
                if (grabToggle == true)
                {
                    grabCheck.collider.gameObject.transform.parent = boxHolder;
                    grabCheck.collider.gameObject.transform.position = boxHolder.position;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

                    grabCheck.rigidbody.velocity = Vector2.zero;
                }
                else if (grabToggle == false)
                {
                    grabCheck.collider.gameObject.transform.parent = null;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

                    grabCheck.rigidbody.velocity = Vector2.zero;
                }
            }
        }
    }
}
