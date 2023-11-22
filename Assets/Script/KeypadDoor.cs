using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadDoor : MonoBehaviour
{

    private Animator anim;

    private bool IsAtDoor = false;

    [SerializeField] private TextMeshProUGUI CodeText;
    string codeTextValue = "";
    public string safeCode;
    public GameObject CodePanel;


    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        CodeText.text = codeTextValue;

        if (codeTextValue == safeCode)
        {
            anim.SetTrigger("OpenDoor");
            CodePanel.SetActive(false);
        }

        if (codeTextValue.Length >= 5)
        {
            codeTextValue = "";
        }

        if (Input.GetKey(KeyCode.E) && IsAtDoor == true)
        {
            CodePanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CodePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IsAtDoor = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsAtDoor = false;
        CodePanel.SetActive(false);
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }

    public void Enter()
    {
        if (codeTextValue == safeCode)
        {
            Debug.Log("it's Working");
        }
        else
        {
            Clear();
        }
    }
    public void Clear()
        {
            codeTextValue = "";
        }
    
}