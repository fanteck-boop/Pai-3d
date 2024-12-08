using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float detectionRange = 15f; // Range within which the enemy detects the player
    public float movementSpeed = 3.5f; // Speed of the enemy when following the player
    private bool isDead = false; // To track if the enemy is dead

    private NavMeshAgent navMeshAgent; // NavMeshAgent for pathfinding
    private Animator animator; // Reference to the animator component

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
            return; // If dead, stop updating movement and animations

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            FollowPlayer();
        }
        else
        {
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

    public void Die()
    {
        if (isDead) return; // Ensure the die method is not called multiple times

        isDead = true;
        Debug.Log("Enemy is dead");

        // Play the death animation
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Assume you have a "Die" trigger in the animator
        }

        // Optionally, you can also stop movement here
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
        }

        // Wait for the death animation to finish before enabling ragdoll
        StartCoroutine(WaitForDeathAnimation());
    }

    private System.Collections.IEnumerator WaitForDeathAnimation()
    {
        // Assuming the death animation length is known (e.g., 2 seconds), or you can dynamically check the animation length
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float deathAnimationLength = stateInfo.length; // Get the length of the current animation
            yield return new WaitForSeconds(deathAnimationLength); // Wait until the death animation finishes
        }

        // After the death animation is done, enable ragdoll
        EnableRagdoll();
    }

    void EnableRagdoll()
    {
        // Enable ragdoll physics (assuming ragdoll components are set up on the enemy)
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false; // Enable physics on the ragdoll parts
        }

        foreach (var col in GetComponentsInChildren<Collider>())
        {
            col.enabled = true; // Enable colliders for ragdoll physics
        }

        // Optionally disable the animator once ragdoll physics are active
        if (animator != null)
        {
            animator.enabled = false; // Disable animator to prevent conflicting with ragdoll
        }
    }
}
