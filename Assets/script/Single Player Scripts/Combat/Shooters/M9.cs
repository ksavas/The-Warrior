using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M9 : Shooter {

    public override void Equip()
    {
        base.Equip();
        nameOfWeapon = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.M9_NAME);
    }

}
