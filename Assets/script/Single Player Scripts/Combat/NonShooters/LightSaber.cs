using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : NonShooter {

    public override void Equip()
    {
        base.Equip();
        nameOfWeapon = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.LIGHT_SABER_NAME);
    }
}
