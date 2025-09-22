using UnityEngine;
using UnityEngine.InputSystem;

public class HandBob : MonoBehaviour
{
    [Header("Walking Settings")]
    public float walkBobSpeed = 6f;
    public float walkBobAmountY = 10f;   // vertical (pixels)
    public float walkBobAmountX = 5f;    // horizontal sway (pixels)

    [Header("Sprinting Settings")]
    public float sprintBobSpeed = 10f;
    public float sprintBobAmountY = 20f;
    public float sprintBobAmountX = 10f;

    private RectTransform rectTransform;
    private Vector2 defaultAnchoredPos;
    private float timer;

    private Vector2 moveInput;
    private bool isSprinting;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultAnchoredPos = rectTransform.anchoredPosition;
    }

    // Input System hooks
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) isSprinting = true;
        if (context.canceled) isSprinting = false;
    }

    void Update()
    {
        if (moveInput.magnitude > 0.1f) // moving
        {
            timer += Time.deltaTime * (isSprinting ? sprintBobSpeed : walkBobSpeed);

            float bobAmountY = isSprinting ? sprintBobAmountY : walkBobAmountY;
            float bobAmountX = isSprinting ? sprintBobAmountX : walkBobAmountX;

            rectTransform.anchoredPosition = new Vector2(
                defaultAnchoredPos.x + Mathf.Cos(timer) * bobAmountX, // sway left/right
                defaultAnchoredPos.y + Mathf.Sin(timer) * bobAmountY  // bob up/down
            );
        }
        else
        {
            // Reset smoothly when idle
            rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition,
                defaultAnchoredPos,
                Time.deltaTime * 5f
            );
            timer = 0;
        }
    }
}
