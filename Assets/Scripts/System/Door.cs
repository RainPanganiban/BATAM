using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    public string doorID = "DefaultDoor";
    public bool isLocked = true;

    private void Start()
    {
        // Restore door state if it was already unlocked before
        if (GameManager.Instance != null && GameManager.Instance.completedTasks.Contains("Unlocked_" + doorID))
        {
            isLocked = false;
            Debug.Log($"Door '{doorID}' restored as unlocked.");
        }
    }

    public void Unlock()
    {
        if (!isLocked) return;

        isLocked = false;
        Debug.Log($"Door '{doorID}' unlocked!");
        PopupManager.Instance?.ShowMessage("Door unlocked.");

        // Save unlocked state permanently in GameManager
        if (GameManager.Instance != null)
            GameManager.Instance.completedTasks.Add("Unlocked_" + doorID);
    }
}