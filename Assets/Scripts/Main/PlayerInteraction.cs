using TMPro;
using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private TMP_Text interactionText;

    private WeaponController weaponController;
    private WeaponPickup currentWeaponPickup;

    void Start()
    {
        weaponController = GetComponent<WeaponController>();
        UpdateUIText(""); 
    }

    void Update()
    {
        if (currentWeaponPickup != null && Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpWeapon();
        }
    }

    public void SetNearbyWeapon(WeaponPickup pickup)
    {
        if (currentWeaponPickup == pickup) return;

        if (currentWeaponPickup != null)
        {
            currentWeaponPickup.ToggleHighlight(false);
        }

        currentWeaponPickup = pickup;
        currentWeaponPickup.ToggleHighlight(true);
        UpdateUIText($"Взять {pickup.weaponName} (E)");
    }

    public void ClearNearbyWeapon(WeaponPickup pickup)
    {
        if (currentWeaponPickup == pickup)
        {
            if (currentWeaponPickup != null)
            {
                currentWeaponPickup.ToggleHighlight(false);
            }

            currentWeaponPickup = null;
            UpdateUIText("");
        }
    }

    private void TryPickUpWeapon()
    {
        if (currentWeaponPickup == null)
        {
            UpdateUIText("");
            return;
        }

        int indexToUnlock = currentWeaponPickup.weaponIndex;

        weaponController.UnlockWeapon(indexToUnlock);
        weaponController.EquipWeapon(indexToUnlock);

        currentWeaponPickup.PerformPickup();

        currentWeaponPickup = null;
        UpdateUIText("");
    }

    private void UpdateUIText(string text)
    {
        if (interactionText != null)
        {
            interactionText.text = text;
            interactionText.gameObject.SetActive(!string.IsNullOrEmpty(text));
        }
    }
}