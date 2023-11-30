using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public AnimatorTransision animator;

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
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
