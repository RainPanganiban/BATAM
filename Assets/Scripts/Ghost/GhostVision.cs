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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 leftBoundary = DirFromAngle(-viewAngle / 2, true);
        Vector3 rightBoundary = DirFromAngle(viewAngle / 2, true);

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewRadius);
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool global)
    {
        if (!global)
            angleInDegrees += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
