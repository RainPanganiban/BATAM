using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput;
    private float xRotation = 0f;
    private Camera playerCamera;
    public bool isSprinting { get; private set; }
    public bool canSprint = true;

    private bool isMoving;
    //private float moveCheckDelay = 0.1f;
    //private float moveCheckTimer;

    //Task system
    [SerializeField]  private TaskManager taskManager;
    private bool lookTaskDone = false;
    private bool moveTaskDone = false;
    private bool sprintTaskDone = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (context.performed)
        {
            isMoving = moveInput.magnitude > 0.1f;

            if (!moveTaskDone && isMoving)
            {
                moveTaskDone = true;
                if (taskManager != null)
                {
                    taskManager.MarkTaskCompleted(1);
                }
            }
        }
        else if (context.canceled)
        {
            moveInput = Vector2.zero;
            isMoving = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

        if (!lookTaskDone && lookInput.magnitude > 0.1f)
        {
            lookTaskDone = true;
            if (taskManager != null)
            {
                taskManager.MarkTaskCompleted(0);
            }
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprinting = true;

            if (!sprintTaskDone)
            {
                sprintTaskDone = true;
                if (taskManager != null)
                {
                    taskManager.MarkTaskCompleted(3);
                }
            }
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }

    private void Update()
    {
        //galaw
        float currentSpeed = (isSprinting && canSprint) ? sprintSpeed : walkSpeed;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.SimpleMove(move * currentSpeed);

        //tingin
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        //para di mabali leeg
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.UpdateFootsteps(isMoving, isSprinting);
        }
    }
}
