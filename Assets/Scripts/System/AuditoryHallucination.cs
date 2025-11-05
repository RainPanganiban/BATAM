using UnityEngine;
using System.Collections;

public class AuditoryHallucination : MonoBehaviour
{
    [Header("Sanity Thresholds")]
    [Range(0, 100)] public float mildThreshold = 75f;     // barely any hallucinations
    [Range(0, 100)] public float moderateThreshold = 50f; // audible whispers
    [Range(0, 100)] public float severeThreshold = 25f;   // frequent and loud

    [Header("Audio Settings")]
    public AudioSource hallucinationSource;
    public AudioClip[] whisperClips;
    public AudioClip[] mumbleClips;
    public float minDelay = 10f;  // longest delay between hallucinations
    public float maxDelay = 25f;  // longest delay when sanity is high
    public bool use3DSound = true;

    private bool isPlaying = false;

    void Start()
    {
        if (hallucinationSource == null)
        {
            hallucinationSource = gameObject.AddComponent<AudioSource>();
            hallucinationSource.spatialBlend = use3DSound ? 1f : 0f; // 1 = 3D, 0 = 2D
            hallucinationSource.playOnAwake = false;
        }

        StartCoroutine(HallucinationLoop());
    }

    IEnumerator HallucinationLoop()
    {
        while (true)
        {
            float sanity = GameManager.Instance.currentSanity;
            float delay = Mathf.Lerp(minDelay, maxDelay, sanity / 100f);

            if (sanity < mildThreshold)
            {
                PlayHallucination(sanity);
            }

            yield return new WaitForSeconds(delay);
        }
    }

    void PlayHallucination(float sanity)
    {
        if (isPlaying || hallucinationSource.isPlaying)
            return;

        AudioClip clipToPlay = null;

        // Pick whisper/mumble depending on sanity
        if (sanity < severeThreshold && mumbleClips.Length > 0)
        {
            clipToPlay = mumbleClips[Random.Range(0, mumbleClips.Length)];
        }
        else if (sanity < moderateThreshold && whisperClips.Length > 0)
        {
            clipToPlay = whisperClips[Random.Range(0, whisperClips.Length)];
        }

        if (clipToPlay != null)
        {
            hallucinationSource.volume = Mathf.Lerp(1f, 0.2f, sanity / 100f); // louder when sanity is lower

            // Randomize 3D position if enabled
            if (use3DSound)
            {
                Vector3 randomOffset = Random.onUnitSphere * Random.Range(1.5f, 4f);
                hallucinationSource.transform.localPosition = randomOffset;
            }

            hallucinationSource.clip = clipToPlay;
            hallucinationSource.Play();
        }
    }
}
