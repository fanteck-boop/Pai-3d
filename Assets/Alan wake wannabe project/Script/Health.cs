using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isVulnerable = false; // Tracks if the enemy can take damage

    // Add a reference to the ragdoll components (e.g., Rigidbody, Collider, Animator)
    public GameObject ragdoll;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (ragdoll != null)
        {
            ragdoll.SetActive(false); // Ensure ragdoll is initially disabled
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isVulnerable)
        {
            Debug.Log("Enemy is invulnerable and cannot be damaged.");
            return;
        }

        currentHealth -= amount;
        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamageBypass(int amount)
    {
        // This method ignores invulnerability and applies damage
        currentHealth -= amount;
        Debug.Log("Enemy Health (Bypass): " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void EnableVulnerability()
    {
        isVulnerable = true;
        Debug.Log(gameObject.name + " is now vulnerable!");
    }

    public void DisableVulnerability()
    {
        isVulnerable = false;
        Debug.Log(gameObject.name + " is now invulnerable!");
    }

    public void ResetVulnerability()
    {
        isVulnerable = false; // Starts as invulnerable
    }

    public bool IsVulnerable()
    {
        return isVulnerable; // Returns the current vulnerability status
    }

    void Die()
    {
        Debug.Log("Enemy " + gameObject.name + " has died!");

        // Switch to ragdoll or disable the regular animation
        if (ragdoll != null)
        {
            ActivateRagdoll();
        }

        // Optionally disable any other behavior, such as movement
        // DisableMovement();
    }

    void ActivateRagdoll()
    {
        // Disable the regular mesh or animations
        if (animator != null)
        {
            animator.enabled = false;
        }

        // Enable the ragdoll and disable the main character model
        ragdoll.SetActive(true);
        gameObject.SetActive(false); // Optionally deactivate the original enemy body
    }

}
