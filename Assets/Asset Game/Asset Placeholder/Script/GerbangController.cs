using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerbangController : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private bool _gateOpen;

    public bool GateOpen
    {
        get
        {
            return _gateOpen;
        }
        private set
        {
            _gateOpen = value;
            animator.SetBool(AnimationString.gateOpen, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            GateOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            GateOpen = false;
        }
    }
}
