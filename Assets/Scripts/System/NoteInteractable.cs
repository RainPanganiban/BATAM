using UnityEngine;
using UnityEngine.Audio;

public class NoteInteractable : MonoBehaviour, IInteractable
{
    [Header("Note Content")]
    [TextArea] public string noteText;
    public Sprite noteImage;

    [Header("Sound Effects")]
    public AudioClip openClip;
    public AudioClip closeClip;

    [Header("Task Trigger (optional)")]
    public bool triggersTask = false;
    public int taskIndexToTrigger = -1; // The task index in your TaskManager

    private bool isOpen = false;

    public void Interact()
    {
        if (isOpen)
        {
            NoteUI.Instance.CloseNote();
            isOpen = false;
            SoundManager.Instance.PlayNoteClose();
        }
        else
        {
            NoteUI.Instance.OpenNote(noteText, noteImage);
            isOpen = true;
            SoundManager.Instance.PlayNoteOpen();

            if (triggersTask && taskIndexToTrigger >= 0)
                TaskManager.Instance.MarkTaskCompleted(taskIndexToTrigger);
        }
    }

    public string GetPromptText()
    {
        return isOpen ? "Press [E] to Close Note" : "Press [E] to Read Note";
    }
}
