using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorDetection : MonoBehaviour
{
    //[SerializeField] private GameObject lightAmbience;
    //private Animator lightAnimation;

    public bool isDetected = false;
    [SerializeField] private float detectionTime = 3f;
    private Coroutine coroutine;

    private RespawnManager respawnManager;
    private ProfesorMove movement;

    [HideInInspector] public float theTime;
    [HideInInspector] public float theCount;

    private void Awake()
    {
        respawnManager = GetComponent<RespawnManager>();
        //lightAnimation = lightAmbience.AddComponent<Animator>();
        movement = GetComponent<ProfesorMove>();

        theTime = movement.timeStop;
        theCount = movement.stopCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartDetectionTimer();
            isDetected = true;
            movement.isStopped = true;
            theTime = theCount;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopDetectionTimer();
            isDetected = false;
            movement.isStopped = false;
        }
    }

    private void StartDetectionTimer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(DetectionTimer());
    }

    private void StopDetectionTimer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
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
