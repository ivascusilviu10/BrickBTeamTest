using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound()
    {
        Debug.Log("PlaySound() called!");

        if (audioSource != null)
        {
            Debug.Log("AudioClip: " + audioSource.clip);
            Debug.Log("IsPlaying: " + audioSource.isPlaying);
            audioSource.Play();
        }
    }
}

