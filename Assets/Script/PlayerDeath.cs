using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private RespawnManager respawnManager;

    private void Start()
    {
        respawnManager = FindObjectOfType<RespawnManager>();

        if (respawnManager == null)
        {
            Debug.LogError("RespawnManager not found! Make sure it exists in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            respawnManager.PlayerDeath(other.gameObject);
        }
    }

}
