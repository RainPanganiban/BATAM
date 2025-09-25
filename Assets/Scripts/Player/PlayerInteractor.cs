using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference interactAction;

    [Header("Raycast")]
    public float maxDistance = 3f;
    public LayerMask interactLayer;

    [Header("UI")]
    public TextMeshProUGUI promptText;
    public GameObject crosshairDot;

    Camera cam;
    IInteractable currentTarget;

    private void Awake()
    {
        cam = Camera.main;
    }

    void OnEnable()
    {
        if (interactAction != null && interactAction.action != null)
        {
            interactAction.action.Enable();
            interactAction.action.performed += OnInteract;
        }
    }

    void OnDisable()
    {
        if (interactAction != null && interactAction.action != null)
            interactAction.action.performed -= OnInteract;
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentTarget = interactable;
                promptText.text = interactable.GetPromptText();
                promptText.gameObject.SetActive(true);
                crosshairDot.SetActive(true);
                return;
            }
        }

        // If nothing interactable is hit
        currentTarget = null;
        promptText.gameObject.SetActive(false);
        crosshairDot.SetActive(true); // still show dot even if no target
    }

    void OnInteract(InputAction.CallbackContext ctx)
    {
        currentTarget?.Interact();
    }

}
