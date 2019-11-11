using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRifle : Shooter {

    public override void Equip()
    {
        base.Equip();
        nameOfWeapon = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.ALIEN_RIFLE_NAME);
    }
}
