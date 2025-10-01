using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        // Always face the camera
        if (mainCam != null)
            transform.forward = mainCam.transform.forward;
    }
}
