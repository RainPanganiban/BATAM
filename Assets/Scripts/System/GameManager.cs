using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stats")]
    public float maxSanity = 100f;
    public float currentSanity;

    public float maxStamina = 100f;
    public float currentStamina;

    [Header("Inventory")]
    public string[] inventory = new string[3]; // 3 slots

    void Awake()
    {
        // Make sure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists across scenes
        }
        else
        {
            Destroy(gameObject); // prevents duplicates
        }
    }

    void Start()
    {
        // Initialize if not already set
        if (currentSanity == 0) currentSanity = maxSanity;
        if (currentStamina == 0) currentStamina = maxStamina;
    }

    public void DecreaseSanity(float amount)
    {
        currentSanity = Mathf.Max(0, currentSanity - amount);
    }

    public void IncreaseSanity(float amount)
    {
        currentSanity = Mathf.Min(maxSanity, currentSanity + amount);
    }

    public void DecreaseStamina(float amount)
    {
        currentStamina = Mathf.Max(0, currentStamina - amount);
    }

    public void IncreaseStamina(float amount)
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + amount);
    }
}
