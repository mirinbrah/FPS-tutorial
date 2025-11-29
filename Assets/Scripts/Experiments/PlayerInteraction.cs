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
        if (weaponController == null) return;
        weaponController.UnequipWeapon();
    }

    void Update()
    {
        CheckInput();
    }

    public void SetNearbyWeapon(WeaponPickup pickup)
    {
        currentWeaponPickup = pickup;
        currentWeaponPickup.ToggleHighlight(true);
        if (interactionText) interactionText.text = $"Взять {pickup.weaponName} (E)";
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
            if (interactionText) interactionText.text = "";
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentWeaponPickup != null)
        {
            if (weaponController == null) return;

            int indexToUnlock = currentWeaponPickup.weaponIndex;

            weaponController.UnlockWeapon(indexToUnlock);

            Destroy(currentWeaponPickup.gameObject);

            currentWeaponPickup = null;
            if (interactionText) interactionText.text = "";

            weaponController.EquipWeapon(indexToUnlock);
        }
    }
}