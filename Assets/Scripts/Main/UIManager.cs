using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Health playerHealth; 

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI velocityYText;
    [SerializeField] private Image healthBarFill; 

    void Update()
    {
        if (playerMovement != null)
        {
            float hSpeed = playerMovement.CurrentHorizontalSpeed;
            float vSpeed = playerMovement.CurrentVerticalVelocity;

            speedText.text = $"Speed: {hSpeed:F1} m/s";
            velocityYText.text = $"Velocity Y: {vSpeed:F1} m/s";
        }

        if (playerHealth != null)
        {
            float fillValue = playerHealth.CurrentHealth / playerHealth.MaxHealth;
            healthBarFill.fillAmount = fillValue;
        }
    }
}