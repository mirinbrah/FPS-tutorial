using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public string weaponName;

    [Header("Visuals")]
    public WeaponModel weaponPrefab;

    [Header("Shooting")]
    public float damage;
    public float range;
    public float fireRate; 
    public int bulletsPerShot;
    public float spread; 

    [Header("State")]
    public bool isAutomatic;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileLifetime = 3f;
}