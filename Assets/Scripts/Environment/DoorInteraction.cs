using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [Header("Scene")]
    public string sceneToLoad;
    public string spawnPointName;

    private Door doorScript;

    private void Start()
    {
        doorScript = GetComponent<Door>();
    }

    public void Interact()
    {
        CombinationDoor comboDoor = GetComponent<CombinationDoor>();
        if (comboDoor != null)
        {
            if (comboDoor.isLocked)
            {
                // Show keypad UI for code entry
                KeypadUI.Instance?.ShowKeypad(comboDoor);
                return;
            }
        }

        if (doorScript != null && doorScript.isLocked)
        {
            // Door is locked — show popup instead of entering
            PopupManager.Instance?.ShowMessage("The door is locked.");
            Debug.Log($"Door '{doorScript.doorID}' is locked.");
            return; // stop here
        }

        Debug.Log("Next spawn point: " + spawnPointName);
        if (SpawnPointManager.Instance != null)
        {
            SpawnPointManager.Instance.SetNextSpawn(spawnPointName);
        }
        else
        {
            Debug.LogWarning("SpawnPointManager missing");
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    public string GetPromptText()
    {
        return "interact to enter \n [ E ]";
    }

}
