using UnityEngine;
using UnityEngine.UIElements;

public class ItemInteraction : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    [SerializeField] private TaskManager taskManager;

    private void Start()
    {
        // Find the TaskManager once
        taskManager = FindObjectOfType<TaskManager>();
    }

    public void Interact()
    {
        if (itemData == null) return;

        PlayerInventory inventory = Object.FindFirstObjectByType<PlayerInventory>();

        if (itemData.triggerTaskOnly)
        {
            Debug.Log($"Triggered task-only item: {itemData.itemName}");

            if (taskManager != null && itemData.taskIndex >= 0)
            {
                taskManager.MarkTaskCompleted(itemData.taskIndex);
            }

            Destroy(gameObject);
            return;
        }

        if (inventory != null)
        {
            if (inventory.PickupItem(itemData))
            {
                Debug.Log("Picked up: " + itemData.itemName);

                if (taskManager != null && itemData.taskIndex >= 0)
                {
                    taskManager.MarkTaskCompleted(itemData.taskIndex);
                }

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
        return "pick up the " + (itemData != null ? itemData.itemName : "Item") + "\n [ E ]";
    }

}
