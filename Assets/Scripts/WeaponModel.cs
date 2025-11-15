using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    [Header("Grip Points")]
    public Transform rightHandGrip;
    public Transform thumb_Target;
    public Transform index_Target;
    public Transform middle_Target;
    public Transform ring_Target;
    public Transform pinky_Target;

    [Header("Shooting point")]
    public Transform shootingPoint;
}