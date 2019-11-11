using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Destructible {

    

    //[SerializeField] EnemyPlayer enemyPlayer;
    [SerializeField] EnemyController enemyController;
    [SerializeField] Scanner scanner;
    [SerializeField] Ragdoll ragDoll;
    public bool damageHasTaken = false;
    public Vector3 bulletPosition;
    public override void Die()
    {
        base.Die();

        enemyController = GetComponent<EnemyPlayer>().enemyController;
        SecondGameManager.Instance.EventBus.RaiseEvent("EnemyDeath");
        ragDoll.EnableRagdoll(true);
        enemyController.OnDeath();
        GetComponent<NavMeshAgent>().enabled = false;
    }

    public override void TakeDamage(float amont, Projectile projectile)
    {
        base.TakeDamage(amont, projectile);
        damageHasTaken = true;
    }
  /*  public override void TakeDamage(float amont)
    {
        base.TakeDamage(amont);
        damageHasTaken = true;
    }*/
    public void GetBulletPosition(Vector3 position)
    {
        bulletPosition = position;
    }
}
