using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string trackName = "PlayButton";

    public void Play()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMusic(trackName, 1f); // pornește muzica DOAR aici
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}
