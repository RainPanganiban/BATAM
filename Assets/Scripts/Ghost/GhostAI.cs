using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public enum State { Patrol, Stalk, Chase }
    public State currentState = State.Patrol;

    [Header("References")]
    public Transform player;
    public GhostVision vision;
    private NavMeshAgent agent;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;
    private float patrolWait = 3f;
    private float patrolTimer;
    private Vector3 lastMoveDirection = Vector3.forward;

    [Header("Chase Settings")]
    public float chaseSpeed = 2.5f;
    public float baseSpeed = 2.5f;

    [Header("Detection Timing")]
    public float timeToSpotPlayer = 2f;   // Must see player for 2s to chase
    private float visibleTimer = 0f;
    public float loseSightTime = 2f;
    public float loseTimer = 0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        agent.updatePosition = true;
        patrolTimer = patrolWait;
        currentState = State.Patrol;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Stalk:
                Stalk();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        agent.speed = baseSpeed;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 0)
            {
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                agent.destination = patrolPoints[patrolIndex].position;
                patrolTimer = patrolWait;
            }
        }

        Vector3 velocity = agent.velocity;
        if (velocity.sqrMagnitude > 0.01f)
        {
            Vector3 moveDir = velocity.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 5f);
        }

        if (vision.CanSeePlayer(player))
        {
            visibleTimer += Time.deltaTime;

            if (visibleTimer >= timeToSpotPlayer)
            {
                currentState = State.Chase;
                visibleTimer = 0f;
            }
        }
        else
        {
            // If ghost can't see player, reset timer slowly
            visibleTimer = Mathf.Max(0, visibleTimer - Time.deltaTime);
        }
    }

    private void Stalk()
    {
        transform.LookAt(player);

        if (vision.CanSeePlayer(player))
        {
            currentState = State.Chase;
        }
    }

    private void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        Vector3 velocity = agent.velocity;
        if (velocity.sqrMagnitude > 0.01f)
        {
            Vector3 moveDir = velocity.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 5f);
        }

        if (!vision.CanSeePlayer(player))
        {
            loseTimer += Time.deltaTime;
            if (loseTimer >= loseSightTime)
            {
                currentState = State.Patrol;
                loseTimer = 0f;
            }
        }
        else
        {
            loseTimer = 0f;
        }
    }
}
