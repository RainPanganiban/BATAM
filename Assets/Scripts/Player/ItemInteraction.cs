using UnityEngine;
using UnityEngine.UIElements;

public class ItemInteraction : MonoBehaviour, IInteractable
{
    public ItemData itemData;

    public void Interact()
    {

        PlayerInventory inventory = Object.FindFirstObjectByType<PlayerInventory>();

        if (inventory != null && itemData != null)
        {
            if (inventory.PickupItem(itemData))
            {
                Debug.Log("picked up: " + itemData.itemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }
    }

    public string GetPromptText()
    {
        return "pick up the " + (itemData != null ? itemData.itemName : "Item");
    }

}
