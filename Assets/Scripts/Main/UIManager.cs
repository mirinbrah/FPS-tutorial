using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Player playerHealth;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI speedText; 
    [SerializeField] private TextMeshProUGUI velocityYText;
    [SerializeField] private Image healthBarFill;

    void Update()
    {
        float horizontalSpeed = playerMovement.CurrentHorizontalSpeed;
        float verticalVelocity = playerMovement.CurrentVerticalVelocity;

        speedText.text = $"Speed: {horizontalSpeed:F1} m/s";
        velocityYText.text = $"Velocity Y: {verticalVelocity:F1} m/s";
        healthBarFill.fillAmount = playerHealth.CurrentHealth / playerHealth.MaxHealth;
    }
}
