using UnityEngine;
using System.Collections;

public class SceneAutoSpawner : MonoBehaviour
{
    [Tooltip("Player prefab to instantiate in this scene")]
    public GameObject playerPrefab;

    private IEnumerator Start()
    {
        // wait one frame so scene objects are initialized
        yield return null;

        if (SpawnPointManager.Instance == null)
        {
            Debug.LogWarning("SceneAutoSpawner missing SpawnPointManager.");
            yield break;
        }

        string spawnName = SpawnPointManager.Instance.ConsumeNextSpawn();

        if (!string.IsNullOrEmpty(spawnName))
        {
            Debug.Log("SceneAutoSpawner: Looking for spawn point: " + spawnName);
            GameObject sp = GameObject.Find(spawnName);

            if (sp != null)
            {
                InstantiateAtPlayer(sp.transform);
                Debug.Log("SceneAutoSpawner: Spawned player at: " + sp.name);
                yield break; // correct way to end coroutine
            }
            else
            {
                Debug.LogWarning("SceneAutoSpawner: Spawn point not found: " + spawnName);
            }
        }
        else
        {
            Debug.Log("SceneAutoSpawner: No spawn name set — spawning at default location.");
        }

        // fallback: instantiate at world origin or this object’s position
        InstantiateAtPlayer(transform);
        yield break; // ends coroutine cleanly
    }

    private void InstantiateAtPlayer(Transform spawnTransform)
    {
        if (playerPrefab == null)
        {
            Debug.LogError("SceneAutoSpawner: playerPrefab is not assigned!");
            return;
        }

        GameObject player = Instantiate(playerPrefab, spawnTransform.position, spawnTransform.rotation);
        player.name = playerPrefab.name; // keeps hierarchy clean
    }
}
