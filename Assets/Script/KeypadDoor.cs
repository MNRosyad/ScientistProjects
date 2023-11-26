using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class KeypadDoor : MonoBehaviour
{

    private Animator anim;
    public Animator transition;
    public float transtionTime = 1f;

    private bool IsAtDoor = false;
    private bool KodeBenar = false;

    [SerializeField] private TextMeshProUGUI CodeText;
    string codeTextValue = "";
    public string safeCode;
    public GameObject CodePanel;
    

    private BoxCollider2D keypadCollider;


    void Awake()
    {
        anim = GetComponent<Animator>();
        keypadCollider = GetComponent<BoxCollider2D>();

    }


    void Update()
    {
        CodeText.text = codeTextValue;

        if (codeTextValue == safeCode)
        {
            anim.SetTrigger("OpenDoor");
            keypadCollider.size = new Vector2 (1.1f, 1.3f);
            keypadCollider.offset = new Vector2 (0.02f, -0.3f);
            CodePanel.SetActive(false);

            KodeBenar = true; 
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

        if (other.tag == "Player" )
        {
            IsAtDoor = true;
            if(KodeBenar)
            {
                GantiScene();
            }
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

    private void GantiScene()
    {
        StartCoroutine (LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
   IEnumerator LoadLevel(int LevelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transtionTime);
        SceneManager.LoadScene(LevelIndex); 
    }

}