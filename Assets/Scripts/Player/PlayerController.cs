using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;
    private Camera playerCamera;
    private bool isSprinting = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }

    private void Update()
    {
        //galaw
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
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

    }
}
