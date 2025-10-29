using UnityEngine;

public class NoteInteractable : MonoBehaviour, IInteractable
{
    [Header("Note Content")]
    [TextArea] public string noteText;
    public Sprite noteImage;

    [Header("Task Trigger (optional)")]
    public bool triggersTask = false;
    public int taskIndexToTrigger = -1; // The task index in your TaskManager

    private bool isOpen = false;

    public void Interact()
    {
        if (isOpen)
        {
            // Close the note
            NoteUI.Instance.CloseNote();
            isOpen = false;
        }
        else
        {
            // Open the note
            NoteUI.Instance.OpenNote(noteText, noteImage);
            isOpen = true;

            // Trigger task if needed
            if (triggersTask && taskIndexToTrigger >= 0)
                TaskManager.Instance.MarkTaskCompleted(taskIndexToTrigger);
        }
    }

    public string GetPromptText()
    {
        return isOpen ? "Press [E] to Close Note" : "Press [E] to Read Note";
    }
}
