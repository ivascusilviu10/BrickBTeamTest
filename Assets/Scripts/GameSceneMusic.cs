using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPlayButton : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // setează numele corect al scenei
    public string trackName = "PlayButton";     // numele piesei din MusicLibrary

    public void OnPlayButtonPressed()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.PlayMusic(trackName, 1f); // reîncarcă melodia

        SceneManager.LoadScene(gameSceneName);
    }
}
