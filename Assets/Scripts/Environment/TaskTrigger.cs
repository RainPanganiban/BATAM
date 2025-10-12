using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [Header("Task Info")]
    public int taskIndex = -1; 
    public bool oneTime = true;

    [SerializeField] private TaskManager taskManager;
    private bool triggered = false;

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();

        GetComponent<Collider>().isTrigger = true;

        if (GameManager.Instance != null && taskIndex >= 0)
        {
            string key = $"{GameManager.Instance.currentChapterIndex}-{taskIndex}";

            if (GameManager.Instance.completedTasks.Contains(key))
            {
                triggered = true;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || taskManager == null || taskIndex < 0) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player entered task trigger area: {gameObject.name}");

            taskManager.MarkTaskCompleted(taskIndex);

            if (oneTime)
            {
                triggered = true;

                gameObject.SetActive(false);
            }
        }
    }

}
