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

    private int selectedSlot = -1;
    private PlayerControl controls;

    void Awake()
    {
        controls = new PlayerControl();

        // Listen for input actions
        controls.UI.SelectSlot1.performed += ctx => SelectSlot(0);
        controls.UI.SelectSlot2.performed += ctx => SelectSlot(1);
        controls.UI.SelectSlot3.performed += ctx => SelectSlot(2);
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        // Make sure all highlights are hidden at start
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

    void SelectSlot(int index)
    {
        // Disable highlight on all slots first
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].highlight != null)
                slots[i].highlight.gameObject.SetActive(false);
        }

        // Enable highlight only on the selected one
        if (index >= 0 && index < slots.Length && slots[index].highlight != null)
        {
            slots[index].highlight.gameObject.SetActive(true);
            selectedSlot = index;
            Debug.Log($"Selected slot {index + 1}");
        }
    }
}
