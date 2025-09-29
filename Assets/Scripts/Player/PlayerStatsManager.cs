using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public PlayerController playerController;

    [Header("Settings")]
    public float sanityDrainRate = 0.5f; 
    public float sanityRestoreRate = 2f; 
    public float staminaDrainRate = 10f; 
    public float staminaRegenRate = 5f;  

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

        if(playerController.isSprinting && GameManager.Instance.currentStamina > 0f)
        {
            GameManager.Instance.DecreaseStamina(staminaDrainRate * Time.deltaTime);

            if (GameManager.Instance.currentStamina <= 0)
            {
                GameManager.Instance.currentStamina = 0f;
                playerController.canSprint = false;
            }
        }
        else
        {
            GameManager.Instance.IncreaseStamina(staminaRegenRate * Time.deltaTime);

            if(GameManager.Instance.currentStamina >= 5f)
            {
                playerController.canSprint = true;
            }
        }

    }
}
