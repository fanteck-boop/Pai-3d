using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player or object to follow
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Offset relative to the player
    public float followSpeed = 5f; // Speed at which the camera follows the player
    public float rotationSpeed = 5f; // Speed at which the camera rotates to align with the player

    private void LateUpdate()
    {
        if (target == null) return;

        // Smoothly move the camera to the target position + offset
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Smoothly rotate the camera to match the target's rotation
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
