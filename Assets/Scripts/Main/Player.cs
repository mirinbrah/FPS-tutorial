using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] private float maxHealth = 1000f;
    private float currentHealth;

    private float damageCooldown = 1f;
    private float lastDamageTime = -1f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (Time.time < lastDamageTime + damageCooldown)
        {
            return; 
        }

        lastDamageTime = Time.time;

        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log($"Игрок получил {amount} урона. Осталось здоровья: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
    }
}