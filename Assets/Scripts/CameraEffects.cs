using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraEffects : MonoBehaviour
{
    public Camera playerCam;
    public float normalFOV = 60f;
    public float sprintFOV = 70f;
    public float fovSpeed = 5f;

    private bool isSprinting;


    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) isSprinting = true;
        if (context.canceled) isSprinting = false;
    }

    private void Update()
    {

        float targetFOV = isSprinting ? sprintFOV : normalFOV;
        playerCam.fieldOfView = Mathf.Lerp(
            playerCam.fieldOfView,
            targetFOV,
            fovSpeed * Time.deltaTime
            ); 
    }
}
