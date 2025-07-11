
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }
        void Stop()
        {
            PauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            Paused = true;
        if (MusicManager.Instance != null)
            MusicManager.Instance.PauseMusic();


    }
        public void Play()
        {
            PauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            Paused = false;
        if (MusicManager.Instance != null)
            MusicManager.Instance.ResumeMusic();
    }
        public void MainMenuButton()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.StopMusic(); 

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}



    

