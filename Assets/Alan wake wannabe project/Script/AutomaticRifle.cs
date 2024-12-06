using UnityEngine;

public class AutomaticRifle : GunBase
{
    protected override void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
        {
            DealDamage(hit.transform, damage);
            Debug.Log("Automatic Rifle hit: " + hit.collider.name);
        }
    }
}
