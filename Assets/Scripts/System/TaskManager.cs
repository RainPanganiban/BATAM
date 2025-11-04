using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public class Chapter
    {
        public string chapterName;
        public List<string> tasks = new List<string>();
    }

    private static TaskManager instance;
    public static TaskManager Instance => instance;

    public TextMeshProUGUI taskText;
    public List<Chapter> chapters;

    private int currentChapterIndex = 0;
    private int currentTaskIndex = 0;
    private HashSet<string> completedTasks = new HashSet<string>();
    private bool initialized = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        InitializeTasks();
    }

    private void InitializeTasks()
    {
        if (initialized) return;

        LoadProgressFromGameManager();
        TryFindTaskText();
        SkipCompletedTasks();
        DisplayCurrentTask();

        initialized = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryFindTaskText();

        if (scene.name == "MainMenu")
        {
            if (taskText != null)
                taskText.gameObject.SetActive(false);
            return;
        }

        // Re-enable UI when entering gameplay scene
        if (taskText != null)
            taskText.gameObject.SetActive(true);

        // Check if we just came from Main Menu (fresh start)
        if (GameManager.Instance != null &&
            GameManager.Instance.currentTaskIndex == 0 &&
            GameManager.Instance.completedTasks.Count == 0)
        {
            ResetTasks();
        }

        DisplayCurrentTask();
    }

    // Automatically find the Task Text UI by name if missing
    private void TryFindTaskText()
    {
        if (taskText == null)
        {
            GameObject found = GameObject.Find("TaskText");
            if (found != null)
            {
                taskText = found.GetComponent<TextMeshProUGUI>();
                Debug.Log("TaskManager: Linked to TaskText UI successfully.");
            }
            else
            {
                Debug.LogWarning("TaskManager: Could not find TaskText in scene.");
            }
        }
    }

    private void LoadProgressFromGameManager()
    {
        if (GameManager.Instance == null) return;

        currentChapterIndex = GameManager.Instance.currentChapterIndex;
        currentTaskIndex = GameManager.Instance.currentTaskIndex;
        completedTasks = GameManager.Instance.completedTasks ?? new HashSet<string>();
    }

    private void SaveProgressToGameManager()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.currentChapterIndex = currentChapterIndex;
        GameManager.Instance.currentTaskIndex = currentTaskIndex;
        GameManager.Instance.completedTasks = completedTasks;
    }

    public void ResetTasks()
    {
        currentChapterIndex = 0;
        currentTaskIndex = 0;
        completedTasks.Clear();
        SaveProgressToGameManager();
        DisplayCurrentTask();
    }

    void DisplayCurrentTask()
    {
        TryFindTaskText(); // ensure the text is linked

        if (taskText == null) return;

        if (currentChapterIndex < chapters.Count &&
            currentTaskIndex < chapters[currentChapterIndex].tasks.Count)
        {
            taskText.text = $"Chapter Task:\n\n{chapters[currentChapterIndex].tasks[currentTaskIndex]}";
        }
        else
        {
            taskText.text = "All tasks completed.";
        }
    }

    public void MarkTaskCompleted(int taskIndex)
    {
        string key = $"{currentChapterIndex}-{taskIndex}";

        if (completedTasks.Contains(key))
            return;

        completedTasks.Add(key);
        SaveProgressToGameManager();

        if (taskIndex == currentTaskIndex)
        {
            AdvanceToNextTask();
        }
    }

    private void AdvanceToNextTask()
    {
        currentTaskIndex++;

        if (currentChapterIndex < chapters.Count &&
            currentTaskIndex >= chapters[currentChapterIndex].tasks.Count)
        {
            currentChapterIndex++;
            currentTaskIndex = 0;
        }

        SkipCompletedTasks();
        SaveProgressToGameManager();
        DisplayCurrentTask();
    }

    private void SkipCompletedTasks()
    {
        string key = $"{currentChapterIndex}-{currentTaskIndex}";

        while (completedTasks.Contains(key))
        {
            currentTaskIndex++;

            if (currentChapterIndex < chapters.Count &&
                currentTaskIndex >= chapters[currentChapterIndex].tasks.Count)
            {
                currentChapterIndex++;
                currentTaskIndex = 0;
            }

            key = $"{currentChapterIndex}-{currentTaskIndex}";
        }
    }
}
