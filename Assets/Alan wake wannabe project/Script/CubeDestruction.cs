using UnityEngine;

public class CubeDestruction : MonoBehaviour
{
    private float collisionTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            collisionTime = Time.time;
            Destroy(other.gameObject, 4f);
        }
    }
}