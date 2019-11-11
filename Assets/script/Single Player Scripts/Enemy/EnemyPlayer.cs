
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyState))]
public class EnemyPlayer : GenericPlayer  {

    [SerializeField] public Scanner playerScanner;
    [SerializeField] public GenericPlayer priorityTarget;
    [SerializeField] SwatSoldier swatProperties;

    public EnemyController enemyController;
    PathFinder pathfinder;
    EnemyShoot enemyShoot;

    bool isDead = false;

    List<GenericPlayer> targets;

    int lookingTargetTime = 2;
    public event System.Action<GenericPlayer> OnTargetSelected;

    private EnemyHealth m_EenemyHealth;
    public EnemyHealth enemyHealth
    {
        get
        {
            if (m_EenemyHealth == null)
                m_EenemyHealth = GetComponent<EnemyHealth>();
            return m_EenemyHealth;
        }

    }
    [SerializeField]
    public EnemyState enemyState;


    void Start()
    {

        enemyController = GetComponentInParent<EnemyController>();
        enemyShoot = GetComponent<EnemyShoot>();
        pathfinder = GetComponent<PathFinder>();
        pathfinder.agent.speed = swatProperties.walkSpeed;
        playerScanner.OnScanReady += Scanner_OnScanReady;
        Scanner_OnScanReady();
        enemyHealth.OnDeath += EnemyHealth_OnDeath;
        enemyState.OnModeChanged += EnemyState_OnModeChanged;
        if (GameManager.Instance.difficulty == GameManager.Difficulty.MEDIUM)
        {
            playerScanner.scanField = playerScanner.scanField + 45f; 
            playerScanner.GetComponent<SphereCollider>().radius = playerScanner.GetComponent<SphereCollider>().radius * 2;
            lookingTargetTime = 4;
        }
        else if (GameManager.Instance.difficulty == GameManager.Difficulty.HARD)
        {
            playerScanner.scanField = playerScanner.scanField * 2f;
            playerScanner.GetComponent<SphereCollider>().radius = playerScanner.GetComponent<SphereCollider>().radius * 2;
            lookingTargetTime = 6;
        }
            

    }
    public void EnemyState_OnModeChanged(EnemyState.EnemyMode state)
    {
        pathfinder.agent.speed = swatProperties.walkSpeed;

        if (state == EnemyState.EnemyMode.AWARE)
            pathfinder.agent.speed = swatProperties.runSpeed;
    }
    private void EnemyHealth_OnDeath()
    {
        isDead = true;
    }
    public void Scanner_OnScanReady()
    {

        if (!playerScanner.isActiveAndEnabled)
            return;
        if (priorityTarget != null )
            return;

        targets = playerScanner.ScanForTargets<GenericPlayer>();

        if (targets.Count == 2)
            priorityTarget = targets[1];
        else
            SelectClosestTarget(targets);

        if (priorityTarget != null)
        {
            if (OnTargetSelected != null)
            {
                //pathfinder.agent.Stop();
                this.enemyState.currentMode = EnemyState.EnemyMode.AWARE;
                OnTargetSelected(priorityTarget);
                enemyShoot.EnemyPlayer_OnTargetSelected(priorityTarget);
                
            }
        }
            
    }

    void SelectClosestTarget(List<GenericPlayer> targets)
    {
        float closestTarget = playerScanner.scanRange;
        foreach (var possibleTarget in targets)
        {
            if (Vector3.Distance(transform.position, possibleTarget.transform.position) < 0.5f)
                continue;

            if (Vector3.Distance(transform.position, possibleTarget.transform.position) < closestTarget)
                priorityTarget = possibleTarget;
        }
    }

    void Update()
    {



        if (!enemyHealth.isAlive)
        {
            print("dsfsdf");
            pathfinder.agent.Stop();
            return;
        }

        if (enemyHealth.damageHasTaken && priorityTarget == null)
        {
            if (isDead)
                return;

            pathfinder.agent.Stop();

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, enemyHealth.bulletPosition, 0, 0);

            transform.rotation =  Quaternion.LookRotation(newDirection);


            playerScanner.scanField = 360;

            playerScanner.GetComponent<SphereCollider>().radius = playerScanner.GetComponent<SphereCollider>().radius * 2;
            SecondGameManager.Instance.Timer.Add(() => 
            {
                try
                {
                    playerScanner.GetComponent<SphereCollider>().radius = playerScanner.GetComponent<SphereCollider>().radius / 2;
                    playerScanner.scanField = 90;
                }
                catch (System.Exception e)
                {
                    return;
                }
                
                if(enemyHealth.isAlive)
                   pathfinder.agent.Resume();
               // transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 30f, transform.rotation.z);
            }, lookingTargetTime);
            enemyHealth.damageHasTaken = false;
            enemyHealth.bulletPosition = Vector3.zero;
        }            

        if (priorityTarget == null)
            return;
        transform.LookAt(priorityTarget.transform.position);

    }
    public void CheckContinuePatrol()
    {

        if (priorityTarget != null)
            return;
        try
        {
            if (pathfinder.agent.isActiveAndEnabled)
                pathfinder.agent.Resume();
        }
        catch (System.Exception e)
        {        }
        
    }
    public void CheckEaseWeapon()
    {
        if (priorityTarget != null)
            return;

        this.enemyState.currentMode = EnemyState.EnemyMode.UNAWARE;

    }
    internal void ClearTargetAndScan()
    {
        if (enemyHealth.hitPointsRemaining < 2)
            return;
        
        priorityTarget = null;

        SecondGameManager.Instance.Timer.Add(CheckEaseWeapon, Random.Range(2,6));
        SecondGameManager.Instance.Timer.Add(CheckContinuePatrol, Random.Range(3, 6));

        Scanner_OnScanReady();
    }
}
