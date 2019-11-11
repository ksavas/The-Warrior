using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destructible {

    public override void Die()
    {
        base.Die();
    }

    public override void TakeDamage(float amont, Projectile projectile)
    {
        base.TakeDamage(amont, projectile);
    }
}
