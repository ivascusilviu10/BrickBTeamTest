using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    void Start()
    {
        Debug.Log("MainMenuMusic Start");
        if (MusicManager.Instance != null)
        {
            Debug.Log("Calling PlayMusic from MainMenuMusic.cs");
            MusicManager.Instance.PlayMusic("PlayButton", 1f);
        }
        else
        {
            Debug.LogError("MusicManager.Instance is NULL!");
        }
    }
}

