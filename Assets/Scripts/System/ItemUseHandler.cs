using UnityEngine;
using UnityEngine.InputSystem;

public class ItemUseHandler : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventoryUI inventoryUI;

    private PlayerControl controls;

    private void Awake()
    {
        controls = new PlayerControl();

        // Listen for the UseItem input (Left Click)
        controls.Player.UseItem.performed += ctx => UseSelectedItem();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        if (playerInventory == null)
            playerInventory = GetComponent<PlayerInventory>();

        if (inventoryUI == null)
            inventoryUI = FindObjectOfType<InventoryUI>();
    }

    void UseSelectedItem()
    {
        if (KeypadUI.Instance != null && KeypadUI.Instance.IsActive)
            return;

        if (playerInventory == null || inventoryUI == null)
        {
            Debug.LogWarning("Missing PlayerInventory or InventoryUI reference.");
            return;
        }

        int selectedIndex = inventoryUI.GetSelectedSlotIndex();
        if (selectedIndex < 0 || selectedIndex >= playerInventory.slots.Length)
        {
            Debug.Log("No item selected.");
            return;
        }

        ItemData item = playerInventory.slots[selectedIndex];
        if (item == null)
        {
            Debug.Log("Selected slot is empty.");
            return;
        }

        if (item.itemEffect != null)
        {
            item.itemEffect.Use(gameObject);

            // If the item restores sanity, consume it
            if (item.itemEffect is RestoreSanityEffect)
            {
                playerInventory.slots[selectedIndex] = null;
                GameManager.Instance.inventorySlots[selectedIndex] = null;
                inventoryUI.RefreshUI();
            }

            // Keep keys by default
        }
        else
        {
            Debug.Log($"{item.itemName} has no effect assigned.");
        }
    }
}
