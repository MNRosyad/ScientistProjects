using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public GameObject respawnUI;
    public Transform respawnPoint;
    public GameObject player;
    public AnimatorTransision animator;
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        respawnUI.SetActive(false);
    }

    public void ShowRespawnUI()
    {
        if (animator != null)
        {
            animator.TransitionCoroutine();
        }
        respawnUI.SetActive(true);
        playerController.EnableMovement(false);
    }

    public void ContinueGame()
    {
        respawnUI.SetActive(false);
        animator.TransitionCoroutine();
        playerController.EnableMovement(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RespawnPlayer()
    {
        player.transform.position = respawnPoint.position;
        ShowRespawnUI();
    }

    public void PlayerDeath(GameObject Player)
    {
        RespawnPlayer();
    }

}