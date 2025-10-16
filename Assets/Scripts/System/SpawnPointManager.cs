using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager Instance;

    public string nextSpawnPointName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetNextSpawn(string spawnName)
    {
        nextSpawnPointName = spawnName;
    }

    public string ConsumeNextSpawn()
    {
        string val = nextSpawnPointName;
        nextSpawnPointName = null;
        return val;
    }
}
