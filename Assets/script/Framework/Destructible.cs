using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Destructible : MonoBehaviour {

    [SerializeField] float hitpoints;

    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;
    float damagaTaken;
    public string killerName;
    public float hitPointsRemaining
    {
        get
        {
            return hitpoints - damagaTaken;
        }
        set
        {
            hitpoints = value;
        }
    }
    public bool isAlive
    {
        get
        {
            return hitPointsRemaining > 0;
        }
    }
    public virtual void Die()
    {
       if (OnDeath != null)
            OnDeath();

    }

    
    public virtual void TakeDamage(float amont)
    {
        if (!isAlive)
            return;
        damagaTaken += amont;
        if (OnDamageReceived != null)
            OnDamageReceived();

        if (hitPointsRemaining <= 0)
        {
            Die();
        }
    }
    public virtual void TakeDamage(float amont,Projectile projectile)
    {
        if (!isAlive)
            return;
        damagaTaken += amont;
        if (GameManager.Instance.isSinglePlayer)
        {
            if (OnDamageReceived != null)
                OnDamageReceived();
        }
        
       if (GameManager.Instance.isSinglePlayer && hitPointsRemaining <= 0)
       {
           Die();

           if (projectile.transform.root.name.Equals("PlayerContainer")){
               projectile.transform.parent.transform.parent.GetComponent<PlayerController>().SetKillScore();
           }
           else
           {
               projectile.transform.parent.GetComponent<EnemyController>().SetKillScore();
           }
       }
            
    }

    public void HealthTaken()
    {
        hitPointsRemaining = hitPointsRemaining + 25;
    }

    public void Reset()
    {
        damagaTaken = 0;
        hitpoints = 100;
        
        if (GameManager.Instance.isSinglePlayer && OnDamageReceived != null)
            OnDamageReceived();
    }

}
