using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{
    PlayerController controller;
    PlatformEffector2D effector;

    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            controller = collision.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (controller == null)
        {
            return;
        }
        else if (controller.passPlatform)
        {
            effector.rotationalOffset = 180;
            controller = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        controller = null;
        effector.rotationalOffset = 0;
    }
}
