using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    public string doorID = "DefaultDoor";
    public bool isLocked = true;

    public void Unlock()
    {
        if (!isLocked)
        {
            Debug.Log($"Door '{doorID}' is already unlocked.");
            return;
        }

        isLocked = false;
        Debug.Log($"Door '{doorID}' unlocked!");
    }
}