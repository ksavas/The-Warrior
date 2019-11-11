using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGameManager {

    public event System.Action<Player> OnLocalPlayerJoined;// CAMERADA,AMMOCOUNTER'DA HATA VERİYOR

    private GameObject gameObject;
    private static SecondGameManager m_Instance;

    public bool isPaused { get; set; }

    public static SecondGameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new SecondGameManager();
                m_Instance.gameObject = new GameObject("_gameManager");
                m_Instance.gameObject.AddComponent<Inputcontroller>();
                m_Instance.gameObject.AddComponent<Timer>();
                m_Instance.gameObject.AddComponent<Respawner>();
            }
            return m_Instance;
        }
    }
    private static Inputcontroller m_InputController;
    public Inputcontroller InputController
    {
        get
        {
            if (m_InputController == null)
                m_InputController = m_Instance.gameObject.GetComponent<Inputcontroller>();
            return m_InputController;
        }
    }
    private Player m_localPlayer;
    public Player LocaLPlayer
    {
        get
        {
            return m_localPlayer;
        }
        set
        {
            m_localPlayer = value;
            if (OnLocalPlayerJoined != null)
                OnLocalPlayerJoined(m_localPlayer);
        }
    }
    private Timer m_Timer;
    public Timer Timer
    {
        get
        {
            if (m_Timer == null)
                m_Timer = gameObject.GetComponent<Timer>();
            return m_Timer;
        }
    }

    private EventBus m_EventBus;
    public EventBus EventBus
    {
        get
        {
            if (m_EventBus == null)
                m_EventBus = new EventBus();
            return m_EventBus;
        }
    }

    private Respawner m_Respawner;
    public Respawner Respawner
    {
        get
        {
            if (m_Respawner == null)
                m_Respawner = gameObject.GetComponent<Respawner>();
            return m_Respawner;
        }
    }

}
