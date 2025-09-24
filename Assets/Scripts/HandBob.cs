using UnityEngine;
using UnityEngine.InputSystem;

public class HandBob : MonoBehaviour
{
    [Header("Walking Settings")]
    public float walkBobSpeed = 6f;
    public float walkBobAmountY = 0.05f;   // vertical offset (world units)
    public float walkBobAmountX = 0.05f;   // horizontal sway (world units)

    [Header("Sprinting Settings")]
    public float sprintBobSpeed = 10f;
    public float sprintBobAmountY = 0.1f;
    public float sprintBobAmountX = 0.1f;

    private Vector3 defaultLocalPos;
    private float timer;
    public PlayerController playerController;

    void Start()
    {
        defaultLocalPos = transform.localPosition; // store the original offset relative to the camera
    }

    void Update()
    {

        Vector2 moveInput = playerController.moveInput;
        bool isSprinting = playerController.isSprinting;

        if (moveInput.magnitude > 0.1f) // moving
        {
            timer += Time.deltaTime * (isSprinting ? sprintBobSpeed : walkBobSpeed);

            float bobAmountY = isSprinting ? sprintBobAmountY : walkBobAmountY;
            float bobAmountX = isSprinting ? sprintBobAmountX : walkBobAmountX;

            // Offset in local space (relative to camera)
            transform.localPosition = defaultLocalPos + new Vector3(
                Mathf.Cos(timer) * bobAmountX, // sway left/right
                Mathf.Sin(timer) * bobAmountY, // bob up/down
                0f
            );
        }
        else
        {
            // Reset smoothly when idle
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                defaultLocalPos,
                Time.deltaTime * 5f
            );
            timer = 0;
        }
    }
}
