using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Shooter {

    public override void Equip()
    {
        base.Equip();
        nameOfWeapon = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.SHOTGUN_NAME);
    }
}
