using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParentTrigger))]
public class NonShooter : Weapon {
   
    bool canAttack;

    public override void Attack()
    {
        
        base.Attack();

        canAttack = false;

        if (Time.time < nextAttackAllowed)
            return;

        nextAttackAllowed = Time.time + rateOfAttack;

        audioAttack.Play();
        canAttack = true;
    }

}
