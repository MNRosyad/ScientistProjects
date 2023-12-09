using System.Collections;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    //public GameObject warningLight; // Lampu peringatan
    public float detectionTime = 3f; // Waktu deteksi

    private bool isDetected = false;
    private RespawnManager respawnManager; // Referensi ke RespawnManager

    // Pastikan objek CameraDetector terhubung dengan RespawnManager melalui Inspector Unity
    void Start()
    {
        respawnManager = FindObjectOfType<RespawnManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isDetected = true;
            StartCoroutine(DetectionTimer());
            //ShowWarning();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isDetected = false;
            //ResetWarning();
            StopCoroutine(DetectionTimer());
        }
    }

    private IEnumerator DetectionTimer()
    {
        yield return new WaitForSeconds(detectionTime);
        if (isDetected)
        {
            // Player terdeteksi selama 3 detik, munculkan UI tryagain, dan lakukan tindakan respawn karakter
            respawnManager.RespawnPlayer();
        }
    }

    //private void ShowWarning()
    //{
    //    // Tampilkan peringatan, contohnya nyalakan lampu merah
    //    warningLight.SetActive(true);
    //}

    //private void ResetWarning()
    //{
    //    // Sembunyikan peringatan, contohnya matikan lampu merah
    //    warningLight.SetActive(false);
    //}
}
