using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadDoor_Stage3 : MonoBehaviour
{
    private bool IsAtKeypad = false;
    //private bool KodeBenar = false;

    [SerializeField] private TextMeshProUGUI CodeText;
    string codeTextValue = "";
    public string safeCode;
    public GameObject CodePanel;
    public SceneTransitionKeypad pintuBesi;


    void Update()
    {
        CodeText.text = codeTextValue;

        if (codeTextValue == safeCode)
        {
            CodePanel.SetActive(false);
            //KodeBenar = true;
            pintuBesi.EnableCollider();
            GetComponent<Collider2D>().enabled = false;
        }

        if (codeTextValue.Length >= 5)
        {
            codeTextValue = "";
        }

        if (Input.GetKey(KeyCode.E) && IsAtKeypad == true)
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
            IsAtKeypad = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsAtKeypad = false;
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