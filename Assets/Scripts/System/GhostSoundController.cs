using UnityEngine;

public class GhostSoundController : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource screamSource;
    public AudioSource chaseMusicSource;

    [Header("Audio Clips")]
    public AudioClip screamClip;
    public AudioClip chaseMusicClip;

    private bool hasPlayedScream = false;

    // Called when ghost first spots the player
    public void PlayScream()
    {
        if (screamClip != null && !hasPlayedScream)
        {
            screamSource.clip = screamClip;
            screamSource.Play();
            hasPlayedScream = true;
        }
    }

    // Called when chase starts
    public void PlayChaseMusic()
    {
        if (chaseMusicClip != null)
        {
            chaseMusicSource.clip = chaseMusicClip;
            chaseMusicSource.loop = true;
            if (!chaseMusicSource.isPlaying)
                chaseMusicSource.Play();
        }
    }

    // Called when chase ends
    public void StopChaseMusic()
    {
        if (chaseMusicSource.isPlaying)
        {
            chaseMusicSource.Stop();
        }

        // Reset scream so it can play again next time
        hasPlayedScream = false;
    }
}
