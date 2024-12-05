using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            // Handle death, e.g., destroy the GameObject
            Destroy(gameObject);
        }
    }
}