using System.Collections.Generic;
using UnityEngine;
using TMPro;

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


    private void Awake()
    {
        // Singleton setup — ensures only one TaskManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void Start()
    {
        if (GameManager.Instance != null)
        {
            currentChapterIndex = GameManager.Instance.currentChapterIndex;
            currentTaskIndex = GameManager.Instance.currentTaskIndex;
            completedTasks = GameManager.Instance.completedTasks;
        }

        // ensure we show an unfinished task
        SkipCompletedTasks();
        DisplayCurrentTask();
    }

    void DisplayCurrentTask()
    {
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

        // If already marked complete, skip
        if (completedTasks.Contains(key))
            return;

        completedTasks.Add(key);

        if (GameManager.Instance != null)
            GameManager.Instance.completedTasks = completedTasks;

        // Check if this was the current task
        if (taskIndex == currentTaskIndex)
        {
            AdvanceToNextTask();  // normal progression
        }
        else if (taskIndex > currentTaskIndex)
        {
            // Future task done early — no UI update yet
            return;
        }
        else
        {
            // Task done in the past (no effect)
            return;
        }
    }

    private void AdvanceToNextTask()
    {
        currentTaskIndex++;

        // Move to next chapter if needed
        if (currentChapterIndex < chapters.Count &&
            currentTaskIndex >= chapters[currentChapterIndex].tasks.Count)
        {
            currentChapterIndex++;
            currentTaskIndex = 0;
        }

        // Here's the key fix: skip all already completed tasks
        SkipCompletedTasks();

        // Save progress
        if (GameManager.Instance != null)
        {
            GameManager.Instance.currentChapterIndex = currentChapterIndex;
            GameManager.Instance.currentTaskIndex = currentTaskIndex;
        }

        DisplayCurrentTask();
    }

    private void SkipCompletedTasks()
    {
        string key = $"{currentChapterIndex}-{currentTaskIndex}";

        // Keep advancing until we find a task that isn't done
        while (completedTasks.Contains(key))
        {
            currentTaskIndex++;

            // If end of chapter, move to next
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
