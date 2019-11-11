using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Shooter {



    public override void Equip()
    {
        base.Equip();
        nameOfWeapon = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.RIFLE_NAME);
    }

    public override void Attack()
    {
        base.Attack();
        if (canFire)
        {
                //Fire the gun
        }
    }
}
