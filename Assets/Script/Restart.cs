using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AnimatorTransision))]

public class Restart : MonoBehaviour
{
    public AnimatorTransision animator;

    private void Awake()
    {
        animator = GetComponent<AnimatorTransision>();
    }


    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine (animator.TransitionTimer());
            RestartGame();
        }
    }

    void RestartGame()
    {
      
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
