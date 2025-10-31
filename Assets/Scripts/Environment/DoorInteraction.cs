using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [Header("Scene")]
    public string sceneToLoad;
    public string spawnPointName;
    private bool isTransitioning = false;

    private Door doorScript;

    private void Start()
    {
        doorScript = GetComponent<Door>();
    }

    public void Interact()
    {
        if (isTransitioning) return;
        isTransitioning = true;

        CombinationDoor comboDoor = GetComponent<CombinationDoor>();
        if (comboDoor != null && comboDoor.isLocked)
        {
            PadlockUI.Instance?.ShowPadlock(comboDoor);
            isTransitioning = false; // reset here so it can retry after unlock
            return;
        }

        if (doorScript != null && doorScript.isLocked)
        {
            PopupManager.Instance?.ShowMessage("The door is locked.");
            Debug.Log($"Door '{doorScript.doorID}' is locked.");
            isTransitioning = false;
            return;
        }

        Debug.Log("Next spawn point: " + spawnPointName);
        SpawnPointManager.Instance?.SetNextSpawn(spawnPointName);

        SceneManager.LoadScene(sceneToLoad);
    }

    public string GetPromptText()
    {
        return "interact to enter \n [ E ]";
    }

}
