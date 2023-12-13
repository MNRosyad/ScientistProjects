using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;
    GameObject player;
    Collider2D portalCollider;
    public AnimatorTransision animator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        portalCollider = GetComponent<Collider2D>();
    }

    public void EnableCollider()
    {
        portalCollider.enabled = true;
    }

    public void DisableCollider()
    {
        portalCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TeleportPlayer();
            animator.TransitionCoroutine();
        }
    }
    private void TeleportPlayer()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > 0.5f)
        {
            player.transform.position = destination.transform.position;
        }
    }

}
