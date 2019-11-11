using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    [SerializeField] float weaponSwitchTime;

    [HideInInspector] public bool CanAttack;

    Weapon[] weapons;
    Transform weaponHolster;
    int currentwWeaponIndex;

    public event System.Action<Weapon> OnWeaponSwitch;

    Weapon m_ActiveWeapon;
    public Weapon ActiveWeapon
    {
        get
        {
          return m_ActiveWeapon;
        }
    }

    void Awake()
    {
        CanAttack = true;
        weaponHolster = transform.Find("Weapons");
        weapons = weaponHolster.GetComponentsInChildren<Weapon>();
        if (weapons.Length > 0)
            Equip(0);
    }
    internal void SwitchWeapon(int direciton) // internal classlar kullanabilir
    {
        CanAttack = false;

        currentwWeaponIndex += direciton;

        if (currentwWeaponIndex > weapons.Length - 1)
            currentwWeaponIndex = 0;
        if (currentwWeaponIndex < 0)
            currentwWeaponIndex = weapons.Length - 1;

        SecondGameManager.Instance.Timer.Add(() => { Equip(currentwWeaponIndex); }, weaponSwitchTime);

        Equip(currentwWeaponIndex);
    }

    void DeactivateWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
            weapons[i].transform.SetParent(weaponHolster);
        }
    }
   internal void Equip(int index)
    {
        DeactivateWeapons();
        CanAttack = true;
        m_ActiveWeapon = weapons[index];
        m_ActiveWeapon.Equip();
        weapons[index].gameObject.SetActive(true);
        if (OnWeaponSwitch != null)
            OnWeaponSwitch(m_ActiveWeapon);
    }
}
