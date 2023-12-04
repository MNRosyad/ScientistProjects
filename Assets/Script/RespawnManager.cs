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
    }

    public void ContinueGame()
    {
        respawnUI.SetActive(false);
        animator.TransitionCoroutine();
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