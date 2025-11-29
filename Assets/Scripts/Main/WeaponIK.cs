using UnityEngine;
using UnityEngine.Animations.Rigging; 

public class WeaponIK : MonoBehaviour
{
    [Header("Rig Setup")]
    [SerializeField] private Rig rigLayer; 

    [Header("IK Targets (from Player Rig)")]
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private Transform thumbTarget;
    [SerializeField] private Transform indexTarget;
    [SerializeField] private Transform middleTarget;
    [SerializeField] private Transform ringTarget;
    [SerializeField] private Transform pinkyTarget;

    private WeaponModel currentWeapon;
    private float targetWeight = 0f;

    void Update()
    {
        if (rigLayer != null)
        {
            rigLayer.weight = Mathf.Lerp(rigLayer.weight, targetWeight, Time.deltaTime * 10f);
        }
    }

    public void SetCurrentWeapon(WeaponModel weapon)
    {
        currentWeapon = weapon;
        if (currentWeapon != null)
        {
            targetWeight = 1f;
        }
        else
        {
            targetWeight = 0f;
        }
    }

    private void LateUpdate()
    {
        if (currentWeapon == null) return;

        SetTargetTransform(leftHandTarget, currentWeapon.leftHandGrip);
        SetTargetTransform(thumbTarget, currentWeapon.thumb_Grip);
        SetTargetTransform(indexTarget, currentWeapon.index_Grip);
        SetTargetTransform(middleTarget, currentWeapon.middle_Grip);
        SetTargetTransform(ringTarget, currentWeapon.ring_Grip);
        SetTargetTransform(pinkyTarget, currentWeapon.pinky_Grip);
    }

    private void SetTargetTransform(Transform target, Transform source)
    {
        if (target != null && source != null)
        {
            target.position = source.position;
            target.rotation = source.rotation;
        }
    }
}