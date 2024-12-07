using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;     // Maximum health of the player
    private int currentHealth;      // Current health of the player

    void Start()
    {
        currentHealth = maxHealth; // Initialize health to maxHealth at the start
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(30); // Take 30 damage when touched by an enemy
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        // Add any death logic here (e.g., respawn, game over screen)
        // For now, we destroy the player object
        Destroy(gameObject);
    }
}
