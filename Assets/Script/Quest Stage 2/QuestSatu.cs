using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSatu : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField;
    public GameObject QuestUI;
    public PortalController portalA;

    public PlayerController playerController;
    public Animator PortalAnimatorA;
    public Animator PortalAnimatorB;

    private bool isPlayerInRange = false;
    private bool isQuestActive = false;

    public void StartQuest()
    {
        isQuestActive = true;

        if (playerController != null)
        {
            playerController.EnableMovement(false);
        }
    }

    public void CompleteQuest()
    {

        if (playerController != null)
        {
            playerController.EnableMovement(true);
        }
        if (PortalAnimatorA != null)
        {
            PortalAnimatorA.SetTrigger("OpenPortal");
        }

        if (PortalAnimatorB != null)
        {
            PortalAnimatorB.SetTrigger("OpenPortal");
        }

    }
    public void ProcessInput()
    {
        string userInput = inputField.text;
        CheckInput(userInput);
    }

    // Metode untuk memeriksa input
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

                CompleteQuest();
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

            if (playerController != null)
            {
                playerController.EnableMovement(true);
            }
            ResetInputField();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
        {
            if (!inputField.isFocused)
            {
                QuestUI.SetActive(true);
                StartQuest();
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape) && isQuestActive)
        {
            if (!inputField.isFocused)
            {
                QuestUI.SetActive(false);

                if (playerController != null)
                {
                    playerController.EnableMovement(true);
                }
                ResetInputField();
            }

        }
    }
}
