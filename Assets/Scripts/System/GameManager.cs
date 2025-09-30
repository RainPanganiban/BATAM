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
    public ItemData[] inventorySlots = new ItemData[3]; // 3 slots

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
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
