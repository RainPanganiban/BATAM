using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController; // link to movement/praying script

    [Header("Settings")]
    public float sanityDrainRate = 0.5f;   // sanity lost per second when near enemy
    public float sanityRestoreRate = 2f;   // sanity gained per second when praying
    public float staminaDrainRate = 10f;   // stamina lost per second when sprinting
    public float staminaRegenRate = 5f;    // stamina regained per second when resting

    void Update()
    {
        HandleSanity();
        HandleStamina();
    }

    void HandleSanity()
    {
        //*
        //if (playerController.isNearEnemy)
        //{
        //    GameManager.Instance.DecreaseSanity(sanityDrainRate * Time.deltaTime);
        //}
        

        // Praying restores sanity
        //if (playerController.isPraying)
        //{
        //    GameManager.Instance.IncreaseSanity(sanityRestoreRate * Time.deltaTime);
        //}
    }

    void HandleStamina()
    {
        // Sprint drains stamina
        if (playerController.isSprinting)
        {
            GameManager.Instance.DecreaseStamina(staminaDrainRate * Time.deltaTime);
        }
        else
        {
            GameManager.Instance.IncreaseStamina(staminaRegenRate * Time.deltaTime);
        }
    }
}
