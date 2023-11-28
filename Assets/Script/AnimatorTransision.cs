using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTransision : MonoBehaviour
{
    public Animator transition;
    [SerializeField] private float transtionTime = 3f;
  
    public void TransitionCoroutine()
    {
        StartCoroutine(TransitionTimer());
    }

    public IEnumerator TransitionTimer()
    {

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transtionTime);
        
    }
}
