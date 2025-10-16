using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [Header("Scene")]
    public string sceneToLoad;
    public string spawnPointName;

    public void Interact()
    {
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
        return "interact to enter";
    }

}
