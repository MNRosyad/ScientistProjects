using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button startButton;

    void Start()
    {
        // Mendengarkan kejadian video selesai diputar
        videoPlayer.loopPointReached += EndReached;
    }

    // Fungsi yang akan dipanggil saat video selesai diputar
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        // Aktifkan tombol UI
        startButton.gameObject.SetActive(true);
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene("Stage 1");
    }
}
