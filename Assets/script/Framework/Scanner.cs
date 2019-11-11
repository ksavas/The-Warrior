using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Extensions;
using System;

[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour {

    
    [SerializeField] float scanSpeed;
    [SerializeField] [Range(0,360)] float fieldOfView;
    [SerializeField] public LayerMask mask;

    SphereCollider rangeTrigger;

    public float scanField
    {
        get
        {
            return fieldOfView;
        }
        set
        {
            fieldOfView = value;
        }
    }

    public float scanRange
    {
        get
        {
            if (rangeTrigger == null)
                rangeTrigger = GetComponent<SphereCollider>();
            return rangeTrigger.radius;
        }
    }

    public event System.Action OnScanReady;

    void PrepareScan()
    {
        SecondGameManager.Instance.Timer.Add(() => 
        {
            if (OnScanReady != null)
                OnScanReady();
        }, scanSpeed);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(fieldOfView / 2) * GetComponent<SphereCollider>().radius);
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(-fieldOfView / 2) * GetComponent<SphereCollider>().radius);
    }

    Vector3 GetViewAngle(float angle)
    {
        float radian = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

    public List<T> ScanForTargets<T>()
    {
        List<T> targets = new List<T>();

            Collider[] results = Physics.OverlapSphere(transform.position, scanRange);
            for (int i = 0; i < results.Length; i++)
            {
                var player = results[i].transform.GetComponent<T>();
                if (player == null)
                {
                   // print("null"); 
                    continue;
                }

                if (!transform.IsInLineOfSight(results[i].transform.position, fieldOfView, mask, Vector3.up))
                    continue;
                targets.Add(player);
            }

        PrepareScan();
        return targets;

    }

}
