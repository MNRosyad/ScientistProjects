using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteraction : MonoBehaviour
{
    public GameObject bookUI;
    private bool isPlayerInRange = false;   

    void Start()
    {
        if (bookUI != null)
        {
            bookUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange == true)
        {
            bookUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bookUI.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            bookUI.SetActive(false);
        }
    }
    public void ExitBookUI()
    {
        bookUI.SetActive(false);
    }    
}