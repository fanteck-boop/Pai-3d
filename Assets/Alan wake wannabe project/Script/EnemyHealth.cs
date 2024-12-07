using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isVulnerable = false; // Tracks if the enemy can take damage
    private Collider mainCollider;
    private Collider[] ragdollColliders;
    private Animator animator;

    private void Awake()
    {
        // Get the main collider, ragdoll colliders, and animator
        mainCollider = GetComponent<Collider>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        animator = GetComponentInChildren<Animator>();

        // Disable ragdoll by default
        ActivateRagdoll(false);

        // Initialize health
        currentHealth = maxHealth;
    }

    private void ActivateRagdoll(bool status)
    {
        // Enable or disable all ragdoll colliders and rigidbodies
        foreach (Collider col in ragdollColliders)
        {
            if (col != mainCollider) // Ensure the main collider is excluded
            {
                col.enabled = status;
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = !status;
                }
            }
        }

        // Toggle the main collider and animator
        mainCollider.enabled = !status;
        animator.enabled = !status;

        // Disable gravity for the main Rigidbody (if present) when activating ragdoll
        Rigidbody mainRb = GetComponent<Rigidbody>();
        if (mainRb != null)
        {
            mainRb.useGravity = !status;
            mainRb.isKinematic = status;
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
            Die(Vector3.zero); // Default explosion position
        }
    }

    public void TakeDamageBypass(int amount)
    {
        currentHealth -= amount; // Ignores invulnerability
        Debug.Log("Enemy Health (Bypass): " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(Vector3.zero); // Default explosion position
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

    public bool IsVulnerable()
    {
        return isVulnerable; // Return the current vulnerability status
    }

    public void Die(Vector3 explosionPosition)
    {
        Debug.Log("Enemy " + gameObject.name + " has died!");
        // Stop monster sound
        MonsterSound monsterSound = GetComponent<MonsterSound>();
        if (monsterSound != null)
        {
            monsterSound.StopMonsterSound();
        }
        KillEnemy(explosionPosition);
    }

    public void KillEnemy(Vector3 explosionPosition)
    {
        // Enable ragdoll
        ActivateRagdoll(true);

        // Apply explosion force to all rigidbodies in ragdoll colliders
        foreach (Collider col in ragdollColliders)
        {
            if (col != mainCollider)
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(40f, explosionPosition, 3f, 3f, ForceMode.VelocityChange);
                }
            }
        }
    }
}
