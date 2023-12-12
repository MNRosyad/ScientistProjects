using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransitionKeypad : MonoBehaviour
{
    public AnimatorTransision animator;
    GameObject player;
    Collider2D doorCollider;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        doorCollider = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GantiScene();
        }
    }

    public void EnableCollider()
    {
        doorCollider.enabled = true;
    }

    public void DisableCollider()
    {
        doorCollider.enabled = false;
    }

    private void GantiScene()
    {
        animator.TransitionCoroutine();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
