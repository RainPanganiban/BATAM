using UnityEngine;

public class InteractSound : MonoBehaviour
{
    [Header("Sound Settings")]
    [Tooltip("The sound that plays when the object is interacted with.")]
    public AudioClip interactClip;

    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
    }

    // This function can be called from PlayerInteractor or Unity Events
    public void PlayInteractSound()
    {
        if (interactClip != null)
        {
            AudioSource.PlayClipAtPoint(interactClip, transform.position, volume);
        }
        else
        {
            Debug.LogWarning($"No interact sound set for {gameObject.name}");
        }

        Debug.Log("Playing interaction sound!");
    }

}
