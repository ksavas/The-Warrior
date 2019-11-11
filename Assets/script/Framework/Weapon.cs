using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] Transform hand;
    [SerializeField] public EWeaponType eWeaponType;// PREFABLARDA BELİRLENECEK
    [SerializeField] protected AudioController audioAttack;
    [SerializeField] protected float rateOfAttack;
    public string nameOfWeapon;

    protected float nextAttackAllowed;
    public virtual void Equip()
    {

        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public virtual void Attack()
    {
        
    }

}
