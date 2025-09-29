using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public ItemData[] slots = new ItemData[3];

    public bool PickupItem(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                return true;
            }
        }
        return false;
    }
}
