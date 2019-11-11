using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {


    public enum EnemyMode
    {
        AWARE,
        UNAWARE,
        DEAD
    }

    private EnemyMode m_CurrentMode;
    public EnemyMode currentMode
    {
        get
        {
            return m_CurrentMode;
        }
        set
        {
            if (m_CurrentMode == value)
                return;

            m_CurrentMode = value;

            if (OnModeChanged != null)
                OnModeChanged(value);
        }
    }

    public event System.Action<EnemyMode> OnModeChanged;

    void Start()
    {
        currentMode = EnemyMode.UNAWARE;
    }

    void Awake()
    {
        currentMode = EnemyMode.UNAWARE;
    }

}
