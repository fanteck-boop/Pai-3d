using UnityEngine;

public class GunBase : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 1f;

    protected float nextFireTime;

    public void TryShoot()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        Debug.Log("Default gun shooting logic. Override this in derived classes.");
    }

    protected void DealDamage(Transform target, float damage)
    {
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }
    }
}
