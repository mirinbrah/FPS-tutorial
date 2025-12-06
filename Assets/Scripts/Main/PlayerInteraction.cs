using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public TMP_Text interactionText;

    private WeaponController weaponController;
    private WeaponPickup currentWeaponPickup;

    void Start()
    {
        weaponController = GetComponent<WeaponController>();
    }

    void Update()
    {
        CheckInput();
    }

    public void SetNearbyWeapon(WeaponPickup pickup)
    {
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

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentWeaponPickup != null)
        {
            if (weaponController == null) return;

            int indexToUnlock = currentWeaponPickup.weaponIndex;
            weaponController.UnlockWeapon(indexToUnlock);
            weaponController.EquipWeapon(indexToUnlock);

            currentWeaponPickup.PerformPickup();

            currentWeaponPickup = null;
            UpdateUIText("");
        }
    }

    private void UpdateUIText(string text)
    {
        if (interactionText) interactionText.text = text;
    }
}