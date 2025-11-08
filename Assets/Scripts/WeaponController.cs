using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [Header("weapon")]
    [SerializeField] private List<WeaponData> weaponSlots = new List<WeaponData>(3);
    [SerializeField] private WeaponData startingWeapon;
    [SerializeField] private Camera playerCamera;

    [Header("IK on player")]
    [SerializeField] private Rig handIKRig;
    [SerializeField] private Transform rightHandIK_Target;
    [SerializeField] private Transform thumbIK_Target;
    [SerializeField] private Transform indexIK_Target;
    [SerializeField] private Transform middleIK_Target;
    [SerializeField] private Transform ringIK_Target;
    [SerializeField] private Transform pinkyIK_Target;

    [Header("IK - relaxed hand")]
    [SerializeField] private Transform relaxed_rhGrip;
    [SerializeField] private Transform relaxed_thumb;
    [SerializeField] private Transform relaxed_index;
    [SerializeField] private Transform relaxed_middle;
    [SerializeField] private Transform relaxed_ring;
    [SerializeField] private Transform relaxed_pinky;

    [Header("weapon socket")]
    [SerializeField] private Transform weaponSocket;

    private WeaponData currentWeapon;
    private WeaponModel currentWeaponModel;
    private Transform shootingPoint;
    private float nextTimeToFire = 0f;
    private int currentWeaponIndex = -1;

    void Start()
    {
        if (startingWeapon != null)
        {
            int startIndex = weaponSlots.IndexOf(startingWeapon);
            if (startIndex != -1) EquipWeapon(startIndex);
        }
        else
        {
            HolsterWeapon();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (currentWeaponIndex != -1) HolsterWeapon();
            else if (weaponSlots.Count > 0 && weaponSlots[0] != null) EquipWeapon(0);
        }

        if (currentWeapon == null) return;

        bool canShoot = (currentWeapon.isAutomatic ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1"));
        if (canShoot && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            Shoot();
        }
    }

    void LateUpdate()
    {
        bool hasWeapon = currentWeaponModel != null;
        handIKRig.weight = Mathf.Lerp(handIKRig.weight, hasWeapon || relaxed_rhGrip != null ? 1f : 0f, Time.deltaTime * 20f);

        Transform rhGrip, thumb, index, middle, ring, pinky;

        if (hasWeapon)
        {
            rhGrip = currentWeaponModel.rightHandGrip;
            thumb = currentWeaponModel.thumb_Target;
            index = currentWeaponModel.index_Target;
            middle = currentWeaponModel.middle_Target;
            ring = currentWeaponModel.ring_Target;
            pinky = currentWeaponModel.pinky_Target;
        }
        else
        {
            rhGrip = relaxed_rhGrip;
            thumb = relaxed_thumb;
            index = relaxed_index;
            middle = relaxed_middle;
            ring = relaxed_ring;
            pinky = relaxed_pinky;
        }

        SetTarget(rightHandIK_Target, rhGrip);
        SetTarget(thumbIK_Target, thumb);
        SetTarget(indexIK_Target, index);
        SetTarget(middleIK_Target, middle);
        SetTarget(ringIK_Target, ring);
        SetTarget(pinkyIK_Target, pinky);
    }

    void EquipWeapon(int index)
    {
        if (index < 0 || index >= weaponSlots.Count || weaponSlots[index] == null || index == currentWeaponIndex) return;
        if (currentWeaponModel != null) Destroy(currentWeaponModel.gameObject);

        currentWeaponIndex = index;
        currentWeapon = weaponSlots[index];

        currentWeaponModel = Instantiate(currentWeapon.weaponPrefab, weaponSocket);
        currentWeaponModel.transform.localPosition = Vector3.zero;
        currentWeaponModel.transform.localRotation = Quaternion.identity;
        shootingPoint = currentWeaponModel.transform.Find("ShootingPoint");
    }

    void HolsterWeapon()
    {
        if (currentWeaponIndex == -1) return;
        if (currentWeaponModel != null) Destroy(currentWeaponModel.gameObject);

        currentWeaponIndex = -1;
        currentWeapon = null;
        currentWeaponModel = null;
        shootingPoint = null;
    }

    private void Shoot()
    {
        if (shootingPoint == null) return;
        for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
        {
            Vector3 direction = GetShootingDirection();
            Instantiate(currentWeapon.projectilePrefab, shootingPoint.position, Quaternion.LookRotation(direction));
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

    private void SetTarget(Transform ikTarget, Transform gripPoint)
    {
        if (ikTarget != null && gripPoint != null)
        {
            ikTarget.position = gripPoint.position;
            ikTarget.rotation = gripPoint.rotation;
        }
    }
}