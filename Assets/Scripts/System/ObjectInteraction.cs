using UnityEngine;

public class ObjectInteraction : MonoBehaviour, IInteractable
{
    [Header("Animation Settings")]
    public Animator animator;
    public string boolName = "isOpen";
    private bool isOpen = false;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        isOpen = !isOpen;
        animator.SetBool(boolName, isOpen);
    }

    public string GetPromptText()
    {
        return isOpen ? "Press [E] to close" : "Press [E] to open";
    }
}
