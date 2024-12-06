using UnityEngine;

public class Handgun : GunBase
{
    protected override void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
        {
            DealDamage(hit.transform, damage);
            Debug.Log("Handgun hit: " + hit.collider.name);
        }
    }
}
