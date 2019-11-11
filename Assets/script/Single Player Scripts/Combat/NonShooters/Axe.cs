using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : NonShooter {

    public override void Equip()
    {
        base.Equip();
        nameOfWeapon = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.AXE_NAME);
    }
}
