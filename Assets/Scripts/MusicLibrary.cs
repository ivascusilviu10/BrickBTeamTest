using UnityEngine;
[System.Serializable]
public struct MusicTrack
{
    public string trackName;
    public AudioClip clip;
}
public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] tracks;
    public AudioClip GetClipFromName(string trackName)
    {
        foreach (var track in tracks)
        {
            Debug.Log("Checking track: " + track.trackName);
            if (track.trackName == trackName)
            {
                Debug.Log("MATCH FOUND for: " + trackName);
                return track.clip;
            }
        }
        Debug.LogWarning("No match found for: " + trackName);
        return null;
    }
}
