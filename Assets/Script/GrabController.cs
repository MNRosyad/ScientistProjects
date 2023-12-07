using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabController : MonoBehaviour
{
    [SerializeField]
    private Transform boxUp;
    [SerializeField]
    private Transform boxFront;
    [SerializeField]
    private Transform boxDetect;

    [SerializeField]
    private float rayFrontDistance = 1f;
    [SerializeField]
    private float rayUpDistance = 2.2f;
    private int layerNumber = 6;
    private int layerMask;

    public bool grabToggle = false;
    [SerializeField]
    private bool defaultRay = true;
    private Collider2D savedCollider;
    private TouchingDirections touchDirection;
    private PlayerController player;

    private RaycastHit2D boxCheck;

    private void Awake()
    {
        touchDirection = GetComponent<TouchingDirections>();
        player = GetComponent<PlayerController>();

        layerMask = 1 << layerNumber;
    }

    private void Update()
    {
        if (defaultRay)
        {
            boxCheck = Physics2D.Raycast(boxDetect.position, Vector2.left * transform.localScale, rayFrontDistance, layerMask);
        }
        else if (!defaultRay)
        {
            boxCheck = Physics2D.Raycast(boxDetect.position, Vector2.up , rayUpDistance, layerMask);
        }

        Grabbing();
    }

    private void Grabbing()
    {
        Collider2D detectBox = boxCheck.collider;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (grabToggle == false)
            {
                if (detectBox != null && detectBox.CompareTag("Box"))
                {
                    detectBox.transform.parent = boxFront;
                    detectBox.transform.position = boxFront.position;
                    detectBox.GetComponent<Rigidbody2D>().isKinematic = true;
                    detectBox.isTrigger = true;

                    player.SetCapsuleDirection(CapsuleDirection2D.Horizontal);
                    player.AdjustColliderOffset(-0.7f, -0.4f);
                    player.AdjustColliderSize(2.3f, 2f);

                    boxCheck.rigidbody.velocity = Vector2.zero;
                    savedCollider = detectBox;
                    grabToggle = true;
                    //defaultRay = false;
                }
            }
            else if (grabToggle == true)
            {
                if (detectBox != null && detectBox.CompareTag("Box"))
                {
                    savedCollider.transform.parent = null;
                    //savedCollider.transform.position = boxFront.position;
                    savedCollider.GetComponent<Rigidbody2D>().isKinematic = false;
                    savedCollider.isTrigger = false;

                    player.SetCapsuleDirection(CapsuleDirection2D.Vertical);
                    player.AdjustColliderOffset(-0.02f, -0.4f);
                    player.AdjustColliderSize(0.8f, 2f);

                    grabToggle = false;
                    //defaultRay = true;
                }
            }
        }
    }
}
