using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyPatrol : MonoBehaviour {


    public WaypointController waypopintController;
    [SerializeField] float waitTimeMin;
    [SerializeField] float waitTimeMax;

    PathFinder pathFinder;

    private EnemyPlayer m_EnemyPlayer;
    public EnemyPlayer enemyPlayer
    {
        get
        {
            if (m_EnemyPlayer == null)
                m_EnemyPlayer = GetComponent<EnemyPlayer>();
            return m_EnemyPlayer;
        }

    }

    void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        pathFinder.OnDestinationReached += Pathfinder_OnDestinationReached;
        enemyPlayer.enemyHealth.OnDeath += EnemyHealth_OnDeath;
        enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
    }
    void EnemyPlayer_OnTargetSelected(GenericPlayer enemyPlayer)
    {
        if (pathFinder.agent.isActiveAndEnabled)
            pathFinder.agent.Stop();
    }

    void OnDisable()
    {
        pathFinder.OnDestinationReached -= Pathfinder_OnDestinationReached;
        waypopintController.OnWaypointChanged -= WaypointController_OnWaypointChanged;
        enemyPlayer.OnTargetSelected -= EnemyPlayer_OnTargetSelected;
    }

    private void EnemyHealth_OnDeath()
    {
        if (pathFinder.agent.isActiveAndEnabled)
        {
            pathFinder.agent.Stop();
        }
            
    }

    public void WaypointController_OnWaypointChanged(Waypoint waypoint )
    {
        if (pathFinder.agent.isActiveAndEnabled)
            pathFinder.SetTarget(waypoint.transform.position);
    }
    void Start()
    {
        waypopintController.SetNextWaypoint();
    }
    private void Pathfinder_OnDestinationReached()
    {
        if (pathFinder.agent.isActiveAndEnabled)
            SecondGameManager.Instance.Timer.Add(waypopintController.SetNextWaypoint, Random.Range(1, 2));//waitTimeMin, WaitTimeMax
    }
}
