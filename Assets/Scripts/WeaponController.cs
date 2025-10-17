using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private WeaponData currentWeapon;

    private float nextTimeToFire = 0f;

    void Update()
    {
        bool canShoot = (currentWeapon.isAutomatic ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1"));

        if (canShoot && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
        {
            Vector3 direction = GetShootingDirection();
            GameObject projectileObject = Instantiate(currentWeapon.projectilePrefab, shootingPoint.position, Quaternion.LookRotation(direction));

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.damage = currentWeapon.damage;
                projectile.speed = currentWeapon.projectileSpeed;
                projectile.lifetime = currentWeapon.projectileLifetime;
            }
        }
    }

    private Vector3 GetShootingDirection()
    {
        Vector3 direction = playerCamera.transform.forward;

        float spreadX = Random.Range(-currentWeapon.spread, currentWeapon.spread);
        float spreadY = Random.Range(-currentWeapon.spread, currentWeapon.spread);

        direction = Quaternion.Euler(spreadY, spreadX, 0) * direction;

        return direction.normalized;
    }
}