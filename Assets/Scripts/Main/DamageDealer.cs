using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damageAmount = 50f;
    [SerializeField] private float damageInterval = 1f; 
    private float nextDamageTime;

    private void OnTriggerStay(Collider other)
    {
        if (Time.time < nextDamageTime) return;

        Health health = other.GetComponentInParent<Health>();

        if (health != null)
        {
            health.TakeDamage(damageAmount);
            nextDamageTime = Time.time + damageInterval;
        }
    }
}