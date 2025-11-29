using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private WeaponIK weaponIK;

    [SerializeField] private WeaponData pistolData; // Слот 0
    [SerializeField] private WeaponData rifleData;  // Слот 1
    [SerializeField] private WeaponData shotgunData; // Слот 2

    private WeaponData currentWeaponData;
    private WeaponModel currentWeaponInstance;
    private float nextTimeToFire = 0f;

    private WeaponData[] weaponInventory = new WeaponData[3];
    private bool[] isWeaponUnlocked = new bool[3];
    private int currentWeaponIndex = -1;

    void Start()
    {
        weaponInventory[0] = pistolData;
        weaponInventory[1] = rifleData;
        weaponInventory[2] = shotgunData;

        UnequipWeapon();
    }

    void Update()
    {
        HandleWeaponSwitching();

        if (currentWeaponInstance == null || currentWeaponIndex == -1)
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && isWeaponUnlocked[0])
        {
            EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && isWeaponUnlocked[1])
        {
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && isWeaponUnlocked[2])
        {
            EquipWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.H) && currentWeaponInstance != null)
        {
            UnequipWeapon();
        }
    }

    public int UnlockWeapon(int index)
    {
        if (index < 0 || index >= 3) return -1;
        isWeaponUnlocked[index] = true;
        return index;
    }

    public void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= 3 || !isWeaponUnlocked[weaponIndex])
        {
            return;
        }

        if (currentWeaponIndex == weaponIndex)
        {
            UnequipWeapon();
            return;
        }

        UnequipWeapon();

        currentWeaponData = weaponInventory[weaponIndex];
        currentWeaponIndex = weaponIndex;

        GameObject weaponObject = Instantiate(currentWeaponData.prefab, weaponHolder);
        weaponObject.transform.localPosition = Vector3.zero;
        weaponObject.transform.localRotation = Quaternion.identity;

        currentWeaponInstance = weaponObject.GetComponent<WeaponModel>();

        if (weaponIK != null)
        {
            weaponIK.SetCurrentWeapon(currentWeaponInstance);
        }
    }

    public void UnequipWeapon()
    {
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance.gameObject);
        }

        currentWeaponInstance = null;
        currentWeaponData = null;
        currentWeaponIndex = -1;

        if (weaponIK != null)
        {
            weaponIK.SetCurrentWeapon(null);
        }
    }

    private void Shoot()
    {
        if (currentWeaponInstance == null) return;

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