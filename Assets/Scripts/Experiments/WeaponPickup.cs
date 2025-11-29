using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponIndex; // 0 = Пистолет, 1 = Автомат, 2 = Дробовик
    public string weaponName = "Оружие";

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

    public void ToggleHighlight(bool active)
    {

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
        GameObject rootObject = other.transform.root.gameObject;
        if (rootObject.CompareTag("Player"))
        {
            PlayerInteraction player = rootObject.GetComponent<PlayerInteraction>();
            if (player) player.SetNearbyWeapon(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject rootObject = other.transform.root.gameObject;
        if (rootObject.CompareTag("Player"))
        {
            PlayerInteraction player = rootObject.GetComponent<PlayerInteraction>();
            if (player) player.ClearNearbyWeapon(this);
        }
    }
}