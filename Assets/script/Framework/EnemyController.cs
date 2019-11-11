using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour {

    public int killScore=0;
    public int deadScore=0;
    [SerializeField] GameObject currentEnemy;
    [SerializeField] GameObject previousEnemy;
    [SerializeField] SpawnPoints[] spawnPoints;
    [SerializeField] ScoreCounter scoreCounter;
    [SerializeField] public WaypointController waypointContainer;

    void Start()
    {
        GeneratePlayer();
        scoreCounter.AddEnemy(transform.GetSiblingIndex());
    }

    void SpawnAtSpawnPoint()
    {
        spawnPoints = transform.Find("SpawnPointContainer").GetComponentsInChildren<SpawnPoints>();
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.GetChild(2).transform.position = spawnPoints[spawnIndex].transform.position;
        transform.GetChild(2).transform.rotation = spawnPoints[spawnIndex].transform.rotation;

    }
    void GeneratePlayer()
    {
        currentEnemy = Instantiate(Resources.Load("Enemy")) as GameObject;
        currentEnemy.GetComponent<EnemyPatrol>().waypopintController = waypointContainer;
        waypointContainer.OnWaypointChanged += currentEnemy.GetComponent<EnemyPatrol>().WaypointController_OnWaypointChanged;
        currentEnemy.transform.parent = gameObject.transform;
        currentEnemy.name = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.ENEMY_NAME) + " " + transform.GetSiblingIndex().ToString();
        SpawnAtSpawnPoint();
    }
    public void SetKillScore()
    {
        killScore++;
        scoreCounter.UpdateKillScore(transform.GetSiblingIndex() + 1, killScore);
    }
    public void SetDeadScore()
    {
        deadScore++;
        scoreCounter.UpdateDeadScore(transform.GetSiblingIndex() + 1, deadScore);
    }
   public void OnDeath()
    {
        SetDeadScore();
        previousEnemy = transform.GetChild(2).gameObject;
        currentEnemy = null;
        previousEnemy.gameObject.layer = 10;
        var scanner = previousEnemy.GetComponentInChildren<Scanner>();
        var enemyPlayer = previousEnemy.GetComponent<EnemyPlayer>();
        var enemyState = previousEnemy.GetComponent<EnemyState>();
        var pathFinder = previousEnemy.GetComponent<PathFinder>();
        pathFinder.enabled = false;
        enemyState.OnModeChanged -= enemyPlayer.EnemyState_OnModeChanged;
        scanner.enabled = false;
        scanner.OnScanReady -= previousEnemy.GetComponent<EnemyPlayer>().Scanner_OnScanReady;

        SecondGameManager.Instance.Timer.Add(()=>{previousEnemy.transform.GetChild(2).gameObject.SetActive(false); }, 2);

        SecondGameManager.Instance.Timer.delete(enemyPlayer.CheckContinuePatrol);
        SecondGameManager.Instance.Timer.delete(enemyPlayer.CheckEaseWeapon);
        MonoBehaviour[] comps = previousEnemy.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }

        SecondGameManager.Instance.Timer.Add(() =>
        {
            DestroyImmediate(previousEnemy.gameObject);
            previousEnemy = null;
        }, 4);

        SecondGameManager.Instance.Timer.Add(() => { GeneratePlayer(); }, 4);
                
    }
}
