using UnityEngine;

public class GhostVision : MonoBehaviour
{
    public float viewRadius = 10f;
    public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool CanSeePlayer(Transform player)
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2f)
        {
            float distToPlayer = Vector3.Distance(transform.position, player.position);

            if (distToPlayer <= viewRadius)
            {
                if(!Physics.Raycast(transform.position, dirToPlayer, distToPlayer, obstacleMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Set gizmo color
        Gizmos.color = Color.yellow;

        // Draw detection radius circle
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        // Get the ghost’s forward direction in world space
        Vector3 forward = transform.forward;

        // Calculate the left and right boundaries based on current rotation
        Vector3 leftBoundary = DirFromAngle(-viewAngle / 2, false);
        Vector3 rightBoundary = DirFromAngle(viewAngle / 2, false);

        // Draw lines representing the cone edges
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewRadius);

        // Optional: draw a line showing the forward direction
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + forward * viewRadius);
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool global)
    {
        if (!global)
        {
            // Add the object's current Y rotation
            angleInDegrees += transform.eulerAngles.y;
        }
        float rad = angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }
}
