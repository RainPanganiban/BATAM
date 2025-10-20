using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Ambient Sounds")]
    public AudioSource ambientSource;
    public AudioClip exteriorAmbience;
    public AudioClip interiorAmbience;

    [Header("Footstep Sounds (Looping)")]
    public AudioSource footstepSource;
    public AudioClip woodWalkClip;
    public AudioClip woodSprintClip;
    public AudioClip dirtWalkClip;
    public AudioClip dirtSprintClip;

    private AudioClip currentWalkClip;
    private AudioClip currentSprintClip;

    private bool isMoving;
    private bool isSprinting;
    private bool isInside;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject[] interiors = GameObject.FindGameObjectsWithTag("Interior");
        GameObject[] exteriors = GameObject.FindGameObjectsWithTag("Exterior");

        if (interiors.Length > 0)
            SetInterior(true);
        else if (exteriors.Length > 0)
            SetInterior(false);
        else
            SetInterior(false);
    }

    private void SetInterior(bool inside)
    {
        isInside = inside;

        // Ambient
        ambientSource.clip = inside ? interiorAmbience : exteriorAmbience;
        ambientSource.loop = true;
        ambientSource.Play();

        // Footstep setup
        currentWalkClip = inside ? woodWalkClip : dirtWalkClip;
        currentSprintClip = inside ? woodSprintClip : dirtSprintClip;
    }

    public void UpdateFootsteps(bool moving, bool sprinting)
    {
        if (moving && !isMoving)
        {
            // Player just started moving
            isMoving = true;
            PlayFootstep(sprinting);
        }
        else if (!moving && isMoving)
        {
            // Player just stopped
            isMoving = false;
            StopFootstep();
        }
        else if (moving && sprinting != isSprinting)
        {
            // Player switched between walk/sprint
            isSprinting = sprinting;
            PlayFootstep(sprinting);
        }
    }

    private void PlayFootstep(bool sprinting)
    {
        isSprinting = sprinting;
        AudioClip clip = sprinting ? currentSprintClip : currentWalkClip;

        if (clip != null)
        {
            footstepSource.clip = clip;
            footstepSource.loop = true;
            footstepSource.Play();
        }
    }

    private void StopFootstep()
    {
        footstepSource.Stop();
    }
}
