using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 initialForward;

    void Start()
    {
        mainCam = Camera.main;
        initialForward = transform.forward; // store the original lighting direction
    }

    void LateUpdate()
    {
        if (mainCam == null) return;

        Vector3 targetPos = mainCam.transform.position;
        targetPos.y = transform.position.y; // keep it upright

        Vector3 lookDir = (targetPos - transform.position).normalized;

        if (lookDir.sqrMagnitude > 0.001f)
        {
            // only rotate around Y to face camera horizontally
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = lookRotation;
        }
    }
}
