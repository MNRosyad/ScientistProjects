using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest_stage3_2 : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField;
    public GameObject QuestUI;
    public PortalController pintuKayuC;

    public PlayerController playerController;
    public Animator doorAnimatorC;
    public Animator doorAnimatorD;

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
        if (doorAnimatorC != null)
        {
            doorAnimatorC.SetTrigger("OpenDoor");
        }

        if (doorAnimatorD != null)
        {
            doorAnimatorD.SetTrigger("OpenDoor");
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
        string jawabanBenar = "evolusi";

        if (input.Equals(jawabanBenar, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Jawaban Benar!");
            QuestUI.SetActive(false);
            pintuKayuC.EnableCollider();
            GetComponent<Collider2D>().enabled = false;

            CompleteQuest();
        }
        else
        {
            Debug.Log("Jawaban Salah! Input akan dihapus.");
            ResetInputField();
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

                // Panggil EnableMovement dari PlayerController saat keluar dari UI
                if (playerController != null)
                {
                    playerController.EnableMovement(true);
                }

                // Setelah keluar dari UI, pastikan input field direset
                ResetInputField();
            }
                
        }
    }
}
