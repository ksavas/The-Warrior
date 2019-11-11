using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PathFinder : MonoBehaviour {

     public NavMeshAgent agent;

    [SerializeField]  float distanceRemainingTreshold;

    bool m_DestinationReached;
    bool destianationReached
    {
        get
        {
            return m_DestinationReached;
        }
        set
        {
            m_DestinationReached = value;
            if (m_DestinationReached)
            {
                if (OnDestinationReached != null)
                    OnDestinationReached();
            }
        }
    }
    public event System.Action OnDestinationReached;
    void Start()
    {
      //  agent = GetComponent<NavMeshAgent>();
    }
    public void SetTarget(Vector3 target){
        agent.SetDestination(target);
        destianationReached = false; 
    }
    void Update()
    {
        if (destianationReached || !agent.hasPath)
            return;

        if (agent.remainingDistance < distanceRemainingTreshold)
            destianationReached = true;
    }
}
