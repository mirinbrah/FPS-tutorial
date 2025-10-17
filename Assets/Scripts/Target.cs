using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    [Header("Stats")]
    public float health = 50f;

    [Header("UI")]
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 1.5f, 0);

    private TextMeshProUGUI healthText;

    void Start()
    {
        if (healthBarPrefab != null)
        {
            GameObject healthBarInstance = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity, transform);
            healthText = healthBarInstance.GetComponentInChildren<TextMeshProUGUI>();
            UpdateHealthDisplay();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0) health = 0;

        UpdateHealthDisplay();

        if (health <= 0f)
        {
            Die();
        }
    }

    private void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString("F0"); // "F0" форматирует число без знаков после запятой
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}