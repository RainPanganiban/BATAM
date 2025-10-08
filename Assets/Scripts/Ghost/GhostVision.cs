using UnityEngine;

public class GhostVision : MonoBehaviour
{
    public float viewRadius = 10f;
    public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Proximity Detection")]
    public float proximityRadius = 3f;

    public bool CanSeePlayer(Transform player)
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        // Always detect if player is within proximity radius
        if (distToPlayer <= proximityRadius)
            return true;

        // Normal cone-based vision detection
        if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2f)
        {
            if (distToPlayer <= viewRadius)
            {
                Vector3 origin = transform.position + Vector3.up * 0.5f; // eye height
                if (!Physics.Raycast(origin, dirToPlayer, distToPlayer, obstacleMask))
                {
                    return true; // Player visible in cone
                }
            }
        }

        return false; // Player not detected
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

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, proximityRadius);
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
