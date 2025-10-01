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

    [Header("Chase Settings")]
    public float chaseSpeed = 2.5f;
    public float baseSpeed = 2.5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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

        if (vision.CanSeePlayer(player))
        {
            currentState = State.Chase;
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

        if (!vision.CanSeePlayer(player))
        {
            currentState = State.Patrol;
        }
    }
}
