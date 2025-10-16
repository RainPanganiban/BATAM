using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Ambient Sounds")]
    public AudioSource ambientSource;
    public AudioClip exteriorAmbience;
    public AudioClip interiorAmbience;

    /*
    [Header("Footstep Sounds")]
    public AudioSource footstepSource;
    public AudioClip[] walkClips;
    public AudioClip[] sprintClips;
    public float footstepIntervalWalk = 0.5f;
    public float footstepIntervalSprint = 0.3f;

    private float footstepTimer;
    */

    void Awake()
    {
        // Singleton pattern
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

    // Automatically detect scene type when it loads
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject[] interiors = GameObject.FindGameObjectsWithTag("Interior");
        GameObject[] exteriors = GameObject.FindGameObjectsWithTag("Exterior");

        if (interiors.Length > 0)
        {
            SetInterior(true);
        }
        else if (exteriors.Length > 0)
        {
            SetInterior(false);
        }
        else
        {
            Debug.LogWarning("No Interior or Exterior tag found in scene. Defaulting to exterior ambience.");
            SetInterior(false);
        }
    }

    // Ambient handling
    private void SetInterior(bool inside)
    {
        ambientSource.clip = inside ? interiorAmbience : exteriorAmbience;
        ambientSource.loop = true;
        ambientSource.Play();
    }

    /* Footsteps handling
    public void HandleFootsteps(bool isMoving, bool isSprinting)
    {
        if (!isMoving)
        {
            footstepTimer = 0;
            return;
        }

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0f)
        {
            AudioClip[] clips = isSprinting ? sprintClips : walkClips;
            if (clips.Length > 0)
            {
                footstepSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
            }

            footstepTimer = isSprinting ? footstepIntervalSprint : footstepIntervalWalk;
        }
    } 
    */
}
