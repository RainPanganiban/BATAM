using UnityEngine;

public class CombinationDoor : MonoBehaviour
{
    public string doorID = "VaultDoor01";
    public bool isLocked = true;
    public string correctCode = "1987";

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Check if this door was already unlocked before
        if (GameManager.Instance.completedTasks.Contains("Unlocked_" + doorID))
        {
            isLocked = false;
        }
    }

    public void Interact()
    {
        if (isLocked)
        {
            Debug.Log("Door is locked, showing keypad UI");
            KeypadUI.Instance.ShowKeypad(this);  // This line enables your keypad
        }
        else
        {
            Debug.Log("Door is unlocked, player can enter");
            GetComponent<DoorInteraction>().Interact();
        }
    }

    public void TryUnlock(string enteredCode)
    {
        if (enteredCode == correctCode)
        {
            Debug.Log("Correct code! Door unlocked!");
            isLocked = false;
            GameManager.Instance.completedTasks.Add("Unlocked_" + doorID);

            // Optional: play animation
            if (animator != null)
                animator.SetTrigger("Open");

            PopupManager.Instance.ShowMessage("Door unlocked!");
        }
        else
        {
            PopupManager.Instance.ShowMessage("Incorrect code.");
        }
    }

    public string GetPromptText()
    {
        return "Enter Code [E]";
    }
}
