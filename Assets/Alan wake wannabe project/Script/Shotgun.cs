using UnityEngine;

public class Shotgun : GunBase
{
    public int pellets = 10; // Number of pellets per shot
    public float spread = 0.1f; // Spread angle

    protected override void Shoot()
    {
        for (int i = 0; i < pellets; i++)
        {
            Vector3 spreadDirection = transform.forward + new Vector3(
                Random.Range(-spread, spread),
                Random.Range(-spread, spread),
                0
            ).normalized;

            if (Physics.Raycast(transform.position, spreadDirection, out RaycastHit hit, range))
            {
                DealDamage(hit.transform, damage / pellets);
                Debug.Log("Shotgun hit: " + hit.collider.name);
            }
        }
    }
}
