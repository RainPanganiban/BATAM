using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlotUI
    {
        public Image border;
        public Image highlight;
        public Image icon;
    }

    public InventorySlotUI[] slots = new InventorySlotUI[3];
    public PlayerInventory playerInventory;
    public HandManager handManager;

    private int selectedSlot = -1;
    private PlayerControl controls;

    void Awake()
    {
        controls = new PlayerControl();

        controls.UI.SelectSlot1.performed += ctx => SelectSlot(0);
        controls.UI.SelectSlot2.performed += ctx => SelectSlot(1);
        controls.UI.SelectSlot3.performed += ctx => SelectSlot(2);
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].highlight != null)
                slots[i].highlight.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInventory == null) return;

        // Update item icons
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < playerInventory.slots.Length && playerInventory.slots[i] != null)
            {
                slots[i].icon.sprite = playerInventory.slots[i].icon;
                slots[i].icon.enabled = true;
            }
            else
            {
                slots[i].icon.enabled = false;
            }
        }
    }

    public void SelectItem(int slotIndex)
    {
        if (playerInventory == null) return;
        if (slotIndex < 0 || slotIndex >= playerInventory.slots.Length) return;

        // Get the actual item data from the PlayerInventory
        ItemData selectedItem = playerInventory.slots[slotIndex];

        if (selectedItem != null)
            handManager.SetSelectedItem(selectedItem.itemName);
        else
            handManager.SetSelectedItem("");
    }

    void SelectSlot(int index)
    {
        // Turn off all highlights first
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].highlight != null)
                slots[i].highlight.gameObject.SetActive(false);
        }

        // Activate selected highlight
        if (index >= 0 && index < slots.Length && slots[index].highlight != null)
        {
            slots[index].highlight.gameObject.SetActive(true);
            selectedSlot = index;
            Debug.Log($"Selected slot {index + 1}");

            // Tell PlayerInventory to update the hand
            if (playerInventory != null)
                playerInventory.SelectItem(index);
        }
    }

    public int GetSelectedSlotIndex()
    {
        return selectedSlot;
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < playerInventory.slots.Length && playerInventory.slots[i] != null)
            {
                slots[i].icon.sprite = playerInventory.slots[i].icon;
                slots[i].icon.enabled = true;
            }
            else
            {
                slots[i].icon.enabled = false;
            }
        }
    }
}
