using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraDetector : MonoBehaviour
{
    [SerializeField] private GameObject lightObject;
    private Animator CCTV_Light;
    public float detectionTime = 3f;

    private bool isDetected = false;
    private RespawnManager respawnManager;
    private Coroutine detectionCoroutine; // Store the coroutine instance

    void Start()
    {
        respawnManager = FindObjectOfType<RespawnManager>();
        CCTV_Light = lightObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDetectionTimer(); // Start or restart the detection timer
            isDetected = true;
            CCTV_Light.SetBool("Change_Color", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopDetectionTimer(); // Stop the detection timer
            isDetected = false;
            CCTV_Light.SetBool("Change_Color", false);
        }
    }

    private void StartDetectionTimer()
    {
        if (detectionCoroutine != null)
        {
            StopCoroutine(detectionCoroutine); // Stop previous coroutine if it exists
        }
        detectionCoroutine = StartCoroutine(DetectionTimer());
    }

    private void StopDetectionTimer()
    {
        if (detectionCoroutine != null)
        {
            StopCoroutine(detectionCoroutine);
            detectionCoroutine = null; // Reset the coroutine instance
        }
    }

    private IEnumerator DetectionTimer()
    {
        float elapsedTime = 0f;
        while (elapsedTime < detectionTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        if (isDetected)
        {
            respawnManager.RespawnPlayer();
        }
    }
}
