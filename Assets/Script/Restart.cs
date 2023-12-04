using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    PlayerController player;
    public AnimatorTransision animator;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player.RestartScene)
        {
            animator.TransitionCoroutine();
            RestartGame();
        }
    }

    void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
