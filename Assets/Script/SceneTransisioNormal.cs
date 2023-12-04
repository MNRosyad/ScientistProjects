using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransisioNormal : MonoBehaviour
{
    public AnimatorTransision animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GantiScene();
        }
    }
    private void GantiScene()
    {
        animator.TransitionCoroutine();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}

