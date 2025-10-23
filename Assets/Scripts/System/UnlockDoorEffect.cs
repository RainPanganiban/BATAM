using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Effects/Unlock Specific Door")]
public class UnlockDoorEffect : ItemEffect
{
    public string doorID; // the id this key unlocks
    public float searchRadius = 3f;
    public string successMessage = "Door unlocked.";
    public string failMessage = "The key doesn't fit.";

    public override void Use(GameObject user)
    {
        // Find doors near the player
        Collider[] hits = Physics.OverlapSphere(user.transform.position, searchRadius);
        foreach (Collider hit in hits)
        {
            Door door = hit.GetComponent<Door>();
            if (door != null)
            {
                // Correct door
                if (door.doorID == doorID)
                {
                    if (door.isLocked)
                    {
                        door.Unlock();
                        PopupManager.Instance?.ShowMessage(successMessage);

                        // Consume the key
                        ConsumeKey(user, this);

                        return;
                    }
                    else
                    {
                        PopupManager.Instance?.ShowMessage("The door is already unlocked.");
                        return;
                    }
                }
            }
        }

        // No matching locked door nearby
        PopupManager.Instance?.ShowMessage(failMessage);
    }

    private void ConsumeKey(GameObject user, ItemEffect usedEffect)
    {
        PlayerInventory playerInventory = user.GetComponent<PlayerInventory>();
        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();

        if (playerInventory == null || inventoryUI == null) return;

        for (int i = 0; i < playerInventory.slots.Length; i++)
        {
            ItemData item = playerInventory.slots[i];
            if (item != null && item.itemEffect == usedEffect)
            {
                playerInventory.slots[i] = null;
                GameManager.Instance.inventorySlots[i] = null;
                inventoryUI.RefreshUI();
                Debug.Log("Key consumed from slot " + i);
                return;
            }
        }
    }
}
