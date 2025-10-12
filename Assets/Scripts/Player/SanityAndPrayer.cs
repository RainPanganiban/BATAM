using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SanityAndPrayer : MonoBehaviour
{
    [Header("Sanity Settings")]
    public float baseDrainRate = 0.5f; // per second
    public float nearGhostDrainMultiplier = 3f;

    [Header("Prayer Settings")]
    public float prayerRestoreRate = 2f; // per second
    public bool isPraying = false;
    public float ghostDangerRadius = 10f;

    [Header("References")]
    public Transform ghost;
    public PlayerInput playerInput;
    public Slider sanityBar;

    private bool nearGhost = false;
    private PlayerController playerMovement;

    //Task Manager
    [SerializeField] private TaskManager taskManager;
    private bool prayerTaskDone = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerController>();
        taskManager = FindObjectOfType<TaskManager>();

        if (sanityBar != null)
        {
            sanityBar.maxValue = GameManager.Instance.maxSanity;
            sanityBar.value = GameManager.Instance.currentSanity;
        }
    }

    void Update()
    {
        HandleSanityDrain();
        UpdateSanityUI();
    }

    void HandleSanityDrain()
    {
        if (ghost != null)
            nearGhost = Vector3.Distance(transform.position, ghost.position) < ghostDangerRadius;

        if (!isPraying)
        {
            float drainAmount = baseDrainRate * (nearGhost ? nearGhostDrainMultiplier : 1f);
            GameManager.Instance.DecreaseSanity(drainAmount * Time.deltaTime);
        }
        else
        {
            GameManager.Instance.IncreaseSanity(prayerRestoreRate * Time.deltaTime);
        }

        if (GameManager.Instance.currentSanity <= 0)
            Die();
    }

    void UpdateSanityUI()
    {
        if (sanityBar != null)
            sanityBar.value = GameManager.Instance.currentSanity;
    }

    public void OnPray(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartPrayer();

            if (!prayerTaskDone)
            {
                prayerTaskDone = true;

                if (taskManager != null)
                {
                    taskManager.MarkTaskCompleted(4); // task #5 = index 4
                }
            }
        }
        else if (context.canceled)
        {
            StopPrayer();
        }
    }

    void StartPrayer()
    {
        isPraying = true;
        if (playerMovement) playerMovement.enabled = false;

        Debug.Log("Praying started");
    }

    void StopPrayer()
    {
        isPraying = false;
        if (playerMovement) playerMovement.enabled = true;

        Debug.Log("Praying stopped");
    }

    void Die()
    {
        Debug.Log("Player lost all sanity and died.");
        // Trigger death screen / respawn here
    }
}
