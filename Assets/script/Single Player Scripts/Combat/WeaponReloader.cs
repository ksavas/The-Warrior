using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour {

    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;
    [SerializeField] Container inventory;

    Weapon weapon;

    public event System.Action OnAmmoChanged;

    public int shotsFiredInClip;
    bool isReloading;
    System.Guid containerItemId;

    public int RoundsRemainingInClip
    {
        get{
            return clipSize - shotsFiredInClip ;
        }
    }
    public int RoundsRemainingInInventory
    {
        get
        {
            return inventory.GetAmountRemaining(containerItemId);
        }
    }
    void Awake()
    {
        weapon = GetComponent<Weapon>();

        inventory.OnContainerReady += () =>
        {
            containerItemId = inventory.Add(weapon.eWeaponType.ToString(), maxAmmo);
        };
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }
    public void Reload()
    {
        if (isReloading)
            return;

        isReloading = true;

        int amountFromInventory = inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip);

        SecondGameManager.Instance.Timer.Add(() => { ExecuteReload(amountFromInventory);}, reloadTime);


    }
    private void ExecuteReload(int amount) 
    {
        isReloading = false;
        shotsFiredInClip -= amount;
        if (OnAmmoChanged != null)
            OnAmmoChanged();
    }
    public void TakeFireFromClip(int amount)
    {
        shotsFiredInClip += amount;
        HandleOnAmmoChanged();
    }
    public void HandleOnAmmoChanged()
    {
        if (OnAmmoChanged != null)
            OnAmmoChanged();
    }
}
