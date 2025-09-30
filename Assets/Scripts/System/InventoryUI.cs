using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image[] slotImages;
    public PlayerInventory playerInventory;

    private void Update()
    {
        if (playerInventory == null) return;

        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < playerInventory.slots.Length && playerInventory.slots[i] != null)
            {
                slotImages[i].sprite = playerInventory.slots[i].icon;
                slotImages[i].enabled = true;
            }
            else
            {
                slotImages[i].enabled = false;
            }
        }
    }
}
