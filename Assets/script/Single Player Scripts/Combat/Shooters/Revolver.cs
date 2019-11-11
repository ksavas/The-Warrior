using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Shooter {
    public override void Equip()
    {
        base.Equip();
        nameOfWeapon = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.REVOLVER_NAME);
    }

}
