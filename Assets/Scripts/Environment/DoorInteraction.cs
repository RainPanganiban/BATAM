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
            Debug.Log("Door has a combination lock — delegating to CombinationDoor.Interact()");
            comboDoor.Interact();
            return;
        }

        // Otherwise, handle it as a normal door
        if (doorScript != null && doorScript.isLocked)
        {
            PopupManager.Instance?.ShowMessage("The door is locked.");
            Debug.Log($"Door '{doorScript.doorID}' is locked.");
            return;
        }

        // If unlocked, continue scene transition
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
