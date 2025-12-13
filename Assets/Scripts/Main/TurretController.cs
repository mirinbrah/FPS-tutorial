using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform target;
    public Transform firePoint;

    public int bulletsPerShot = 1;
    public float damage = 10f;
    public float speed = 20f;
    public float fireRate = 1f;
    public float spread = 5f;
    public float bulletLifetime = 3f;

    private float nextFireTime;

    void Update()
    {
        if (target == null) return;

        transform.LookAt(target);

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        Vector3 direction = target.position - firePoint.position;
        Quaternion lookRot = Quaternion.LookRotation(direction);

        for (int i = 0; i < bulletsPerShot; i++)
        {
            float rndX = Random.Range(-spread, spread);
            float rndY = Random.Range(-spread, spread);

            Quaternion finalRot = lookRot * Quaternion.Euler(rndX, rndY, 0);

            GameObject bulletObj = Instantiate(projectilePrefab, firePoint.position, finalRot);

            Projectile p = bulletObj.GetComponent<Projectile>();
            if (p != null)
            {
                p.damage = damage;
                p.speed = speed;
                p.lifetime = bulletLifetime;
            }
        }
    }
}