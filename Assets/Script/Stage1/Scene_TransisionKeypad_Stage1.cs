using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene_TransisionKeypad_Stage1 : MonoBehaviour
{
    public AnimatorTransision animator;
    GameObject player;
    Collider2D doorCollider;
    public Animator pintukayu_anim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        doorCollider = GetComponent<Collider2D>();
    }

    public void AnimatorPintuKayu()
    {
        pintukayu_anim.SetTrigger("OpenDoor");
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
