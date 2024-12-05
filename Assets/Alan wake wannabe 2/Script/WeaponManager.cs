using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public int currentWeaponIndex = 0;

    private void Start()
    {
        // Activate the starting weapon
        SwitchWeapon(currentWeaponIndex);
    }

    private void Update()
    {
        // Handle shooting input
        if (Input.GetButtonDown("Fire1")) // Default: Left mouse button
        {
            if (weapons.Count > 0)
            {
                GunBase gun = weapons[currentWeaponIndex].GetComponent<GunBase>();
                if (gun != null)
                {
                    gun.TryShoot();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            GameObject weapon = other.gameObject;

            // Add the weapon to the inventory
            weapons.Add(weapon);

            // Automatically switch to the new weapon
            currentWeaponIndex = weapons.Count - 1;
            SwitchWeapon(currentWeaponIndex);

            // Disable the weapon object in the world
            weapon.SetActive(false);

            // Optional: Destroy the weapon after a short delay
            Destroy(weapon, 0.1f);
        }
    }

    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            currentWeaponIndex = index;

            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(i == currentWeaponIndex);
            }
        }
    }
}
