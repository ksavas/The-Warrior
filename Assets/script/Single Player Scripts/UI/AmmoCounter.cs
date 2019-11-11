using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{

    [SerializeField] Text text;
    [SerializeField] Text text2;
    public Player player;
    [SerializeField] PlayerShoot playerShoot;
    [SerializeField] WeaponReloader reloader;

    void Start()
    {
        SecondGameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;// !! BURAYI ÇÖZ
        //HandleOnLocalPlayerJoined(player);
    }

    public void HandleOnLocalPlayerJoined(Player player)
    {
        playerShoot = player.PlayerShoot;
        playerShoot.OnWeaponSwitch += HandleOnWeaponSwitch;
        HandleOnWeaponSwitch(playerShoot.ActiveWeapon);
    }
    void HandleOnWeaponSwitch(Weapon activeWeapon)
    {

        text2.text = activeWeapon.nameOfWeapon;
        if (activeWeapon.eWeaponType == EWeaponType.NONSHOOT)
        {
            text.text = "∞";
            return;
        }
        reloader = ((Shooter)activeWeapon).reloader;
        reloader.OnAmmoChanged += HandleOnAmmoChanged;
        HandleOnAmmoChanged();
    }
    void HandleOnAmmoChanged()
    {

        int amountInInventory = reloader.RoundsRemainingInInventory;
        int amountInClip = reloader.RoundsRemainingInClip;
        text.text = string.Format("{0}/{1}", amountInClip, amountInInventory);
    }

    void Update()
    {

    }
}
