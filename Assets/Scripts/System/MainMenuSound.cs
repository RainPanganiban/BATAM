using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    [Header("Background Music")]
    public AudioClip backgroundMusic;
    [Range(0f, 1f)] public float bgmVolume = 0.5f;

    [Header("Button Sounds")]
    public AudioClip buttonClick;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private AudioSource musicSource;

    void Awake()
    {
        InitializeAudio();
    }

    void InitializeAudio()
    {
        // Setup BGM AudioSource
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = bgmVolume;
        musicSource.playOnAwake = false;
        musicSource.spatialBlend = 0f; // 2D sound
        musicSource.Play();
    }

    // Call this from your UI buttons
    public void PlayButtonClick()
    {
        if (buttonClick != null)
        {
            AudioSource.PlayClipAtPoint(buttonClick, Camera.main.transform.position, sfxVolume);
        }
    }

}
