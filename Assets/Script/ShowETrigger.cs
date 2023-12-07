using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowETrigger : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer render;
    private GrabController grabControl;

    [SerializeField]
    private GameObject focusObject;
    private Transform theObject;

    [SerializeField]
    private bool _showE = false;

    public bool ShowE
    {
        get
        {
            return _showE;
        }
        private set
        {
            _showE = value;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        theObject = focusObject.GetComponent<Transform>();

        render.enabled = false;
    }

    private void FixedUpdate()
    {
        if (theObject.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1, 1f);
        }
        else if (theObject.transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowE = true;
            render.enabled = true;
            grabControl = collision.gameObject.GetComponent<GrabController>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (grabControl == null)
        {
            return;
        }
        else if (grabControl.grabToggle == false)
        {
            ShowE = true;
            render.enabled = true;
        }
        else if (grabControl.grabToggle == true)
        {
            ShowE = false;
            render.enabled = false;
            grabControl = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ShowE = false;
        render.enabled = false;
        grabControl = null;
    }
}
