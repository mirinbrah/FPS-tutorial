using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private List<WeaponData> availableWeapons;

    private WeaponData currentWeaponData;
    private WeaponModel currentWeaponInstance;
    private float nextTimeToFire = 0f;

    void Update()
    {
        HandleWeaponSwitching();

        if (currentWeaponInstance == null)
        {
            return;
        }

        bool canShoot = (currentWeaponData.isAutomatic ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1"));
        if (canShoot && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / currentWeaponData.fireRate;
            Shoot();
        }
    }

    private void HandleWeaponSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            UnequipWeapon();
        }
    }

    void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= availableWeapons.Count || availableWeapons[weaponIndex] == null)
        {
            return;
        }

        UnequipWeapon();

        currentWeaponData = availableWeapons[weaponIndex];

        GameObject weaponObject = Instantiate(currentWeaponData.prefab, weaponHolder);
        weaponObject.transform.localPosition = Vector3.zero;
        weaponObject.transform.localRotation = Quaternion.identity;

        currentWeaponInstance = weaponObject.GetComponent<WeaponModel>();
    }

    void UnequipWeapon()
    {
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance.gameObject);
            currentWeaponInstance = null;
            currentWeaponData = null;
        }
    }

    private void Shoot()
    {
        if (currentWeaponInstance == null || currentWeaponInstance.shootingPoint == null) return;

        for (int i = 0; i < currentWeaponData.bulletsPerShot; i++)
        {
            Vector3 direction = GetShootingDirection();
            GameObject projectileObject = Instantiate(
                currentWeaponData.projectilePrefab,
                currentWeaponInstance.shootingPoint.position,
                Quaternion.LookRotation(direction)
            );

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.damage = currentWeaponData.damage;
                projectile.speed = currentWeaponData.projectileSpeed;
                projectile.lifetime = currentWeaponData.projectileLifetime;
            }
        }
    }

    private Vector3 GetShootingDirection()
    {
        Vector3 direction = playerCamera.transform.forward;

        float spreadX = Random.Range(-currentWeaponData.spread, currentWeaponData.spread);
        float spreadY = Random.Range(-currentWeaponData.spread, currentWeaponData.spread);

        direction = Quaternion.Euler(spreadY, spreadX, 0) * direction;

        return direction.normalized;
    }
}