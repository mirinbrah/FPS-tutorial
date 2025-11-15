using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damageAmount = 50f;

    private void OnTriggerStay(Collider other)
    {
        Player playerHealth = other.GetComponentInParent<Player>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}