using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField]
    private MusicLibrary musicLibrary;
    [SerializeField]
    private AudioSource musicSource;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else

        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        Debug.Log("[MusicManager] Trying to play: " + trackName);
        AudioClip nextClip = musicLibrary.GetClipFromName(trackName);

        if (nextClip == null)
        {
            Debug.LogWarning("[MusicManager] Clip not found for: " + trackName);
            return;
        }

        if (musicSource.clip == nextClip && musicSource.isPlaying)
        {
            Debug.Log("[MusicManager] Track already playing: " + trackName);
            return;
        }

        Debug.Log("[MusicManager] Starting coroutine for: " + trackName);
        StartCoroutine(AnimateMusicCrossfade(nextClip, fadeDuration));
    }
    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        float targetVolume = 0.038f;

        // Fade out or silence old clip
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(musicSource.volume, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, targetVolume, percent);
            yield return null;
        }
    }
    public void PauseMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
    }

    public void ResumeMusic()
    {
        if (!musicSource.isPlaying)
            musicSource.UnPause();
    }
    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }

}
