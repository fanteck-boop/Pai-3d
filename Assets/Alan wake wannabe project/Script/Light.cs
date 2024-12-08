using System.Collections.Generic; // For HashSet
using UnityEngine;

public class CubeDestruction : MonoBehaviour
{
    public float raycastRange = 10f;   // Range of the raycast
    public float allowedTime = 10f;   // Time required to make the enemy vulnerable
    private Dictionary<GameObject, float> exposureTimes = new Dictionary<GameObject, float>(); // Tracks exposure times of enemies

    private void Update()
    {
        // Cast a ray from the cube's position
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward); // Ray pointing forward from the cube

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                GameObject enemy = hit.collider.gameObject;

                // Update exposure time for this enemy
                if (!exposureTimes.ContainsKey(enemy))
                {
                    exposureTimes[enemy] = 0f; // Start tracking this enemy
                }

                exposureTimes[enemy] += Time.deltaTime;

                // If the enemy has been exposed long enough, make it vulnerable
                if (exposureTimes[enemy] >= allowedTime)
                {
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null && !enemyHealth.IsVulnerable())
                    {
                        enemyHealth.EnableVulnerability();
                    }
                }
            }
        }

        // Check and reset enemies not currently in the raycast
        ResetExposureForOutOfRangeEnemies();
    }

    private void ResetExposureForOutOfRangeEnemies()
    {
        // Create a list to hold enemies that need to be removed from tracking
        List<GameObject> toRemove = new List<GameObject>();

        foreach (var entry in exposureTimes)
        {
            GameObject enemy = entry.Key;

            if (enemy == null)
            {
                // Remove null references
                toRemove.Add(enemy);
                continue;
            }

            // Check if the enemy is no longer in the ray's range
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            if (!Physics.Raycast(transform.position, directionToEnemy.normalized, out RaycastHit hit, raycastRange) || hit.collider.gameObject != enemy)
            {
                // Reset vulnerability and mark for removal
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.DisableVulnerability();
                }

                toRemove.Add(enemy);
            }
        }

        // Remove enemies from the tracking dictionary
        foreach (var enemy in toRemove)
        {
            exposureTimes.Remove(enemy);
        }
    }
}
