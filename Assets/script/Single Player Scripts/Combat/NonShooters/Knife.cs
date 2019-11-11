using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : NonShooter {

    public override void Equip()
    {
        base.Equip();
        nameOfWeapon = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.KNIFE_NAME);
    }

}
