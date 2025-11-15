using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement; 

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI speedText; 
    [SerializeField] private TextMeshProUGUI velocityYText; 

    void Update()
    {
        float horizontalSpeed = playerMovement.CurrentHorizontalSpeed;
        float verticalVelocity = playerMovement.CurrentVerticalVelocity;

        speedText.text = $"Speed: {horizontalSpeed:F1} m/s";
        velocityYText.text = $"Velocity Y: {verticalVelocity:F1} m/s";
    }
}