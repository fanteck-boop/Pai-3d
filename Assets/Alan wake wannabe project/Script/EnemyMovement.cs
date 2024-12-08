using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float detectionRange = 15f; // Range within which the enemy detects the player
    public float movementSpeed = 3.5f; // Speed of the enemy when following the player

    private NavMeshAgent navMeshAgent; // NavMeshAgent for pathfinding
    private Animator animator; // Reference to the animator component
    private bool isPlayerDetected = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Ensure NavMeshAgent and Animator are initialized
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = movementSpeed;
        }

        if (animator != null)
        {
            animator.SetBool("isCrawling", false); // Start in idle animation
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            isPlayerDetected = true;
            FollowPlayer();
        }
        else
        {
            isPlayerDetected = false;
            Idle();
        }
    }

    void FollowPlayer()
    {
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(player.position);
        }

        if (animator != null)
        {
            animator.SetBool("isCrawling", true); // Switch to crawling animation
        }
    }

    void Idle()
    {
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.ResetPath(); // Stop moving
        }

        if (animator != null)
        {
            animator.SetBool("isCrawling", false); // Switch to idle animation
        }
    }
}
