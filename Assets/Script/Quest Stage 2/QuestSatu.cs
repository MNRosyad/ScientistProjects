using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSatu : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField;
    public GameObject QuestUI;
    public PortalController portalA;

    private bool isPlayerInRange = false;


    void Start()
    {
        if (QuestUI != null)
        {
            QuestUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
        {
            QuestUI.SetActive(true);
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
            QuestUI.SetActive(false);

            // Setelah keluar dari jangkauan, pastikan input field direset
            ResetInputField();
        }
    }

    public void ProcessInput()
    {
        string userInput = inputField.text;
        CheckInput(userInput);
    }

    public void CheckInput(string input)
    {
        int jawabanBenar = 7;

        if (int.TryParse(input, out int inputAngka))
        {
            if (input.Length > 2)
            {
                inputField.text = "";
            }
            if (inputAngka == jawabanBenar)
            {
                Debug.Log("Jawaban Benar!");
                QuestUI.SetActive(false);
                portalA.EnableCollider();
                GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                Debug.Log("Jawaban Salah! Input akan dihapus.");
                inputField.text = "";
            }
        }
        else
        {
            Debug.Log("Input bukan angka! Input akan dihapus.");
            inputField.text = "";
        }
    }

    // Metode untuk mereset input field
    public void ResetInputField()
    {
        inputField.text = "";
    }
}
