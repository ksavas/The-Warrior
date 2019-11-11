using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerPlayerHealth : Destructible
{
    Client c;
    public bool isDead = false;
    public override void Die()
    {
        base.Die();
    }

    public override void TakeDamage(float amont, Projectile projectile)// BURADA HITPOINTSREMAINING KONTROLU YAP    
    {

        if (isDead)
        {
            hitPointsRemaining = 100;
            return;
        }
        base.TakeDamage(amont, projectile);
        if (hitPointsRemaining <= 0)
        {
            c = null;
            c = GetClient();
            isDead = true;
            c.Send("CDEAD|" + c.clientId.ToString() + "|" + GetComponentInParent<ServerPlayerController>().clientId);
            return;
        }
        if (c == null)
            c = GetClient();
        c.Send("CDMG|" + GetComponentInParent<ServerPlayerController>().clientId + "|" + hitPointsRemaining.ToString());  

      
    }

    

    private Client GetClient()
    {
        return FindObjectOfType<Client>();
    }
}
