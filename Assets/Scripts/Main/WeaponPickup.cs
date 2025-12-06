using System; 
using UnityEngine;
using UnityEngine.Events;

public class WeaponPickup : MonoBehaviour
{

    public int weaponIndex;
    public string weaponName;

    public UnityEvent OnPlayerPickUp;

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

    public void PerformPickup()
    {
        Debug.Log($"Предмет {weaponName} был подобран и сейчас исчезнет.");
        OnPlayerPickUp.Invoke();
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