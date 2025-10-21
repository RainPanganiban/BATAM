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
        Collider[] hits = Physics.OverlapSphere(user.transform.position, searchRadius);

        foreach (var c in hits)
        {
            Door door = c.GetComponent<Door>();
            if (door != null)
            {
                if (door.doorID == doorID)
                {
                    if (door.isLocked)
                    {
                        door.Unlock();
                        PopupManager.Instance?.ShowMessage(successMessage);
                        Debug.Log($"Unlocked door '{doorID}' with key.");
                    }
                    else
                    {
                        PopupManager.Instance?.ShowMessage("Door is already unlocked.");
                    }
                    return;
                }
            }
        }

        // No matching door found nearby
        PopupManager.Instance?.ShowMessage(failMessage);
        Debug.Log("No matching locked door nearby!");
    }
}
