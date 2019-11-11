using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyAnimation : MonoBehaviour {

    [SerializeField] Animator animator;

    Vector3 lastPosition;

    PathFinder pathFinder;

    EnemyPlayer enemyPlayer;

    private bool m_Iscrouched;
    public bool isCrouched
    {
        get 
        {
            return m_Iscrouched;
        }

        internal set
        {
            m_Iscrouched = value;
            SecondGameManager.Instance.Timer.Add(CheckIsSaveToStandUp,5);// random değer atabilirsin
        }
    }


    void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        enemyPlayer = GetComponent<EnemyPlayer>();
    }

    void Update(){
        float velocity = ((transform.position - lastPosition).magnitude)/Time.deltaTime;
        lastPosition = transform.position;
        animator.SetBool("isWalking", enemyPlayer.enemyState.currentMode == EnemyState.EnemyMode.UNAWARE);    
        animator.SetFloat("Vertical",velocity/pathFinder.agent.speed);
        animator.SetBool("isAiming", enemyPlayer.enemyState.currentMode == EnemyState.EnemyMode.AWARE);
        animator.SetBool("isCrouching", isCrouched);
    }

    void CheckIsSaveToStandUp()
    {
        bool isUnaware = enemyPlayer.enemyState.currentMode == EnemyState.EnemyMode.UNAWARE;
        if (isUnaware)
        {
            isCrouched = false;
        }

    }

}
