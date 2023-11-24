using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    void Update()
    {
        // Cek apakah tombol R ditekan
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Panggil fungsi RestartGame
            RestartGame();
        }
    }

    void RestartGame()
    {
        // Dapatkan indeks scene saat ini
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load ulang scene dengan indeks yang sama
        SceneManager.LoadScene(currentSceneIndex);
    }
}
