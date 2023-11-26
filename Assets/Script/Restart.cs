using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{

    public Animator transition;
    public float transtionTime = 1f;


    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
        {

            StartCoroutine (RestartGameWithTransition());
        }
    }

    IEnumerator RestartGameWithTransition()
    {
      
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transtionTime);
        RestartGame();
    }

    void RestartGame()
    {
      
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
