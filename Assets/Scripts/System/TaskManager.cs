using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public class Chapter
    {
        public string chapterName;
        public List<string> tasks = new List<string>();
    }

    public TextMeshProUGUI taskText;                // Reference to the UI Text
    public List<Chapter> chapters;       // Holds all chapter tasks
    private int currentChapterIndex = 0; // Track which chapter we’re in
    private int currentTaskIndex = 0;

    private HashSet<string> completedTasks;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            currentChapterIndex = GameManager.Instance.currentChapterIndex;
            currentTaskIndex = GameManager.Instance.currentTaskIndex;

            completedTasks = GameManager.Instance.completedTasks;
        }

        SkipCompletedTasks();
        DisplayCurrentTask();
    }

    void DisplayCurrentTask()
    {
        if (currentChapterIndex < chapters.Count &&
            currentTaskIndex < chapters[currentChapterIndex].tasks.Count)
        {
            taskText.text = $"Chapter Task: \n\n {chapters[currentChapterIndex].tasks[currentTaskIndex]}";
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

        if (GameManager.Instance != null)
            GameManager.Instance.completedTasks = completedTasks;

        if (taskIndex == currentTaskIndex)
        {
            AdvanceToNextUncompletedTask();
        }
    }

    private void AdvanceToNextUncompletedTask()
    {
        string key;
        do
        {
            currentTaskIndex++;

            // Move to next chapter if needed
            if (currentChapterIndex < chapters.Count &&
                currentTaskIndex >= chapters[currentChapterIndex].tasks.Count)
            {
                currentChapterIndex++;
                currentTaskIndex = 0;
            }

            key = $"{currentChapterIndex}-{currentTaskIndex}";

        } while (completedTasks.Contains(key) &&
                 currentChapterIndex < chapters.Count &&
                 currentTaskIndex < chapters[currentChapterIndex].tasks.Count);

        // Save new state to GameManager
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

        if (GameManager.Instance != null)
        {
            GameManager.Instance.currentChapterIndex = currentChapterIndex;
            GameManager.Instance.currentTaskIndex = currentTaskIndex;
        }
    }

    public bool IsTaskCompleted(int taskIndex)
    {
        return completedTasks.Contains($"{currentChapterIndex}-{taskIndex}");
    }

    public bool IsCurrentTask(int taskIndex)
    {
        return currentTaskIndex == taskIndex;
    }
}
