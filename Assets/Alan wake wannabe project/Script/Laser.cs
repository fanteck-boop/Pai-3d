using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastVisualize : MonoBehaviour
{
    private GameObject hitObj; // To store the object we hit with the ray
    private LineRenderer raycastLineRenderer; // The LineRenderer to visualize the ray

    // Blood red color (dark red)
    private Color bloodRed = new Color(0.6f, 0.0f, 0.0f, 1f); // Full opacity red color

    // Start is called before the first frame update
    void Start()
    {
        raycastLineRenderer = transform.GetComponent<LineRenderer>(); // Get the LineRenderer attached to the object
        if (raycastLineRenderer == null)
        {
            raycastLineRenderer = gameObject.AddComponent<LineRenderer>(); // Add LineRenderer if not found
        }

        raycastLineRenderer.startWidth = 0.06f;  // Width of the line at the start
        raycastLineRenderer.endWidth = 0.01f;    // Width of the line at the end
        raycastLineRenderer.enabled = false;     // Initially, the ray is hidden

        // Apply an unlit material to the LineRenderer for consistent coloring
        Material lineMaterial = new Material(Shader.Find("Unlit/Color"));
        lineMaterial.color = bloodRed;
        raycastLineRenderer.material = lineMaterial; // Assign the material to the LineRenderer
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit; // To store information about the hit object
        Vector3 direction = transform.forward; // The direction of the raycast (forward direction of the object)

        // Perform the raycast
        if (Physics.Raycast(transform.position, direction, out hit, 100f)) // 100f is the max distance for the ray
        {
            // Draw the ray in the editor (for debugging)
            Debug.DrawRay(transform.position, direction * hit.distance, bloodRed);

            // If the ray hits something, draw the line to that point
            raycastLineRenderer.enabled = true; // Enable the LineRenderer
            raycastLineRenderer.SetPosition(0, transform.position); // Start of the ray
            raycastLineRenderer.SetPosition(1, hit.point); // End of the ray (where it hits the object)
        }
        else
        {
            // If the ray doesn't hit anything, draw a long line ahead
            raycastLineRenderer.enabled = true; // Enable the LineRenderer
            raycastLineRenderer.SetPosition(0, transform.position); // Start of the ray
            raycastLineRenderer.SetPosition(1, transform.position + direction * 100f); // End of the ray (100 units ahead)
        }

        // Set the color of the ray to blood red in both cases (hit or miss)
        raycastLineRenderer.startColor = bloodRed;
        raycastLineRenderer.endColor = bloodRed;
    }
}
