using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestDua : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField1;
    [SerializeField] public TMP_InputField inputField2;
    public GameObject QuestUI;
    public PortalController portalC;

    public PlayerController playerController;
    public Animator PortalAnimatorC;
    public Animator PortalAnimatorD;

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
        if (PortalAnimatorC != null)
        {
            PortalAnimatorC.SetTrigger("OpenPortal");
        }

        if (PortalAnimatorD != null)
        {
            PortalAnimatorD.SetTrigger("OpenPortal");
        }

    }
    public void ProcessInput()
    {
        string userInput1 = inputField1.text;
        string userInput2 = inputField2.text;
        CheckInputs(userInput1, userInput2);
    }


    public void CheckInputs(string input1, string input2)
    {
        int jawabanBenar1 = 45;
        int jawabanBenar2 = 12;

        bool jawaban1Benar = CheckSingleInput(input1, jawabanBenar1);
        bool jawaban2Benar = CheckSingleInput(input2, jawabanBenar2);

        if (jawaban1Benar && jawaban2Benar)
        {
            Debug.Log("Kedua jawaban benar!");
            QuestUI.SetActive(false);
            portalC.EnableCollider();
            GetComponent<Collider2D>().enabled = false;

            CompleteQuest();
        }
        else
        {
            Debug.Log("Setidaknya satu jawaban salah.");
            ResetInputField();
        }
    }

    private bool CheckSingleInput(string input, int jawabanBenar)
    {
        if (int.TryParse(input, out int inputAngka))
        {
            if (input.Length > 2)
            {
                return false;
            }

            return inputAngka == jawabanBenar;
        }

        return false;
    }

    public void ResetInputField()
    {
        inputField1.text = "";
        inputField2.text = "";
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
            if (!inputField1.isFocused)
            {
                QuestUI.SetActive(true);
                StartQuest();
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape) && isQuestActive)
        {
            if (!inputField1.isFocused)
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
