using UnityEngine;
using System; 

public class WeaponPickup : MonoBehaviour
{
    public int weaponIndex;
    public string weaponName;

    public event Action OnPickedUp;

    private Renderer rend;
    private Color originalColor;
    private bool isHighlighted;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    private void OnEnable()
    {
        OnPickedUp += HandlePickup;
    }

    private void OnDisable()
    {
        OnPickedUp -= HandlePickup;
    }

    public void PerformPickup()
    {
        OnPickedUp?.Invoke();
    }

    private void HandlePickup()
    {
        Debug.Log($"Предмет {weaponName} был подобран и сейчас исчезнет.");

        Destroy(gameObject); 
    }

    public void ToggleHighlight(bool active)
    {
        if (rend == null) return;

        if (active && !isHighlighted)
        {
            rend.material.color = originalColor + new Color(0.5f, 0.5f, 0);
            isHighlighted = true;
        }
        else if (!active && isHighlighted)
        {
            rend.material.color = originalColor;
            isHighlighted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out PlayerInteraction player))
        {
            player.SetNearbyWeapon(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.TryGetComponent(out PlayerInteraction player))
        {
            player.ClearNearbyWeapon(this);
        }
    }
}