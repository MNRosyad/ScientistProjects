using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpadSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject jumpPad;

    private void Awake()
    {
        if (jumpPad != null)
        {
            jumpPad.SetActive(false);
        }
        else if (jumpPad == null)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            jumpPad.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            jumpPad.SetActive(false);
        }
    }
}
