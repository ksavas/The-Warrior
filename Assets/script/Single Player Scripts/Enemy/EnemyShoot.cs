using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Extensions;

[RequireComponent(typeof(EnemyPlayer))]
public class EnemyShoot : WeaponController {


    float shootingSpeed;
    float burstDurationMax;
    float burstDurationMin;

    [SerializeField]EnemyPlayer enemyPlayer;

    bool shouldFire;
    
    void Start()
    {
        enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
        switch(GameManager.Instance.difficulty){
            case (GameManager.Difficulty.EASY):
                shootingSpeed = 0.2f;
                burstDurationMin = 0.1f;
                burstDurationMax = 0.3f;
                break;
            case (GameManager.Difficulty.MEDIUM):
                shootingSpeed = 0.1f;
                burstDurationMin = 0.05f;
                burstDurationMax = 0.15f;
                break;
            case(GameManager.Difficulty.HARD):
                shootingSpeed = 0.05f;
                burstDurationMin = 0.025f;
                burstDurationMax = 0.075f;
                break;
        }
    }

    public void EnemyPlayer_OnTargetSelected(GenericPlayer target)
    {

        ((Shooter)ActiveWeapon).AimTarget = target.transform;
        ((Shooter)ActiveWeapon).AimTargetOffset = Vector3.up * 1.5f ;
        StartBurst();
    }

    void CrouchState()
    {
        if (!transform.gameObject)
            return;

        bool takeCover = Random.Range(0, 3) == 0;

        if (!takeCover)
            return;
        float distanceToTarget = Vector3.Distance(transform.position, ((Shooter)ActiveWeapon).AimTarget.position);
        if (distanceToTarget < 15 && CanSeeTarget())
        {
            enemyPlayer.GetComponent<EnemyAnimation>().isCrouched = true;
        }

    }

    void StartBurst()
    {
        if (!transform.gameObject)
            return;
        if (!enemyPlayer.isActiveAndEnabled)
            return;
        if (!enemyPlayer.enemyHealth.isAlive && !CanSeeTarget())
            return;

        CheckReload();
        CrouchState();
        shouldFire = true;

        SecondGameManager.Instance.Timer.Add(EndBurst,Random.RandomRange(burstDurationMin,burstDurationMax));
    }

    void EndBurst()
    {
        if (!transform.gameObject)
            return;
        if (!enemyPlayer.isActiveAndEnabled)
            return;
        shouldFire = false;
        if (!enemyPlayer.enemyHealth.isAlive)
            return;

        CheckReload();
        CrouchState();

        if(CanSeeTarget())
            SecondGameManager.Instance.Timer.Add(StartBurst, shootingSpeed);

    }
    bool CanSeeTarget()
    {
        if (!transform.gameObject)
            return false;

        var enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth.hitPointsRemaining < 2) 
        {
            return false;
        }
        if(!transform.IsInLineOfSight(((Shooter)ActiveWeapon).AimTarget.position,90,enemyPlayer.playerScanner.mask,Vector3.up))
        {
           // var pathFinder = GetComponent<PathFinder>();
           // pathFinder.agent.Resume();
            enemyPlayer.ClearTargetAndScan();
            return false;
        }
        
        return true;
    } 
    void CheckReload()
    {
        if (!transform.gameObject)
            return;

        if (((Shooter)ActiveWeapon).reloader.RoundsRemainingInClip == 0)
        {
            CrouchState();
            ((Shooter)ActiveWeapon).Reload();
        }
    }
    void Update()
    {
        if (!shouldFire || !CanAttack ||!enemyPlayer.enemyHealth.isAlive)
            return;

        ((Shooter)ActiveWeapon).Attack();
    }

}
