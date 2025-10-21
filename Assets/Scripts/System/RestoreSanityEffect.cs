using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Effects/Restore Sanity")]
public class RestoreSanityEffect : ItemEffect
{
    public float sanityRestoreAmount = 20f;

    public override void Use(GameObject user)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IncreaseSanity(sanityRestoreAmount);
            PopupManager.Instance?.ShowMessage($"You've restored some of your sanity.");
        }
        else
        {
            Debug.LogWarning("GameManager instance not found — cannot restore sanity.");
        }
    }
}
