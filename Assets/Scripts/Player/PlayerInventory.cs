using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public ItemData[] slots = new ItemData[4];
    public HandManager handManager;

    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = GameManager.Instance.inventorySlots[i];
        }

        if (handManager == null)
            handManager = FindObjectOfType<HandManager>();
    }

    public bool PickupItem(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                GameManager.Instance.inventorySlots[i] = item;
                return true;
            }
        }
        return false;
    }

    public void SelectItem(int slotIndex)
{
    if (slotIndex < 0 || slotIndex >= slots.Length)
        return;

    ItemData selectedItem = slots[slotIndex];

    if (selectedItem != null)
    {
        handManager.SetSelectedItem(selectedItem.itemName);
    }
    else
    {
        handManager.SetSelectedItem("");
    }
}
}
