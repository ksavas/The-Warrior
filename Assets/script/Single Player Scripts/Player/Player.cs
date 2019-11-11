using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] 
[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : GenericPlayer {


    [System.Serializable]
    public class MouseInput
    {
        public Vector2 damping;
        public Vector2 sensitivity;
        public bool lockMouse;
    }

    [SerializeField] SwatSoldier swatProperties;
    [SerializeField] SpawnPoints[] spawnPoints;
    [SerializeField] MouseInput mouseInputControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimumMovetreshHold;

    public PlayerAim playerAim;



    public bool hasJetPack;

    Vector3 previousPosition;
    //----
    UdpSender client;
    Client c;
    //-----
    Inputcontroller playerInput;
    Vector2 mouseInput;
    
    private CharacterController m_CharacterController;
    public CharacterController MoveController
    {
        get
        {
            if (m_CharacterController == null)
            {
                m_CharacterController = GetComponent<CharacterController>();
            }
            return m_CharacterController;
        }
    }

    private PlayerHealth m_PlayerHealth;
    public PlayerHealth PlayerHealth
    {
        get
        {
            if (m_PlayerHealth == null)
                m_PlayerHealth = GetComponent<PlayerHealth>();
            return m_PlayerHealth;
        }
    }

    private PlayerShoot m_PlayerShoot;
    public PlayerShoot PlayerShoot
    {
        get
        {
            if (m_PlayerShoot == null)
                m_PlayerShoot = GetComponent<PlayerShoot>();
            return m_PlayerShoot;
        }
    }

    private PlayerState m_PlayerState;
    public PlayerState PlayerState
    {
        get
        {
            if (m_PlayerState == null)
                m_PlayerState = GetComponent<PlayerState>();
            return m_PlayerState;
        }
    }
	void Awake () 
    {
        SecondGameManager.Instance.LocaLPlayer = this;
        playerInput = SecondGameManager.Instance.InputController;

        if (mouseInputControl.lockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        PlayerHealth.OnDeath += PlayerHealth_OnDeath;
	}
    void Start()
    {
        spawnPoints = transform.parent.GetComponent<PlayerController>().spawnPoints;
        if (!GameManager.Instance.isSinglePlayer)
        {
            client = FindObjectOfType<UdpSender>();
            c = FindObjectOfType<Client>();
            c.Send("CPOS" + "|" + c.clientId + "|" + transform.position.x.ToString() + "|" + transform.position.y.ToString() + "|" + transform.position.z.ToString());
        }
    }
    void PlayerHealth_OnDeath()
    {
        if(GameManager.Instance.isSinglePlayer)
            transform.parent.GetComponent<PlayerController>().SetDeadScore();
        transform.gameObject.layer = 10;

        if (!GameManager.Instance.isSinglePlayer)
            FindObjectOfType<ServerHealtCounter>().SetHitPoint("100");

        SkinnedMeshRenderer[] meshes = transform.Find("Mesh").transform.GetComponentsInChildren<SkinnedMeshRenderer>();
        MonoBehaviour[] children = GetComponentsInChildren<MonoBehaviour>();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].transform.gameObject.layer = 10;
        }
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].enabled = false;
        }

        SecondGameManager.Instance.Timer.Add(() =>
        {


            PlayerHealth.Reset();
            transform.gameObject.layer = 8;
            SpawnAtSpawnPoint();
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].enabled = true;
            }
            children = GetComponentsInChildren<MonoBehaviour>();
            for (int i = 0; i < children.Length; i++)
            {
                children[i].transform.gameObject.layer = 8;
            }
        }, 3);
    }
	void Update () {



        if (SecondGameManager.Instance.isPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            return;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (!PlayerHealth.isAlive || GameManager.Instance.gameFinished)
            return;
        
        Move();
        LookAround();
    }

    private void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.mouseInput.x, 1f / mouseInputControl.damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.mouseInput.y, 1f / mouseInputControl.damping.y);
        transform.Rotate(Vector3.up * mouseInput.x * mouseInputControl.sensitivity.x);
        playerAim.SetRotation(mouseInput.y * mouseInputControl.sensitivity.y);
    }

    void Move()
    {
        float moveSpeed = swatProperties.runSpeed;
        if (playerInput.isWalking)
            moveSpeed = swatProperties.walkSpeed;
        if (playerInput.isSprinting)
            moveSpeed = swatProperties.sprintSpeed;
        if (playerInput.isCrouching)
            moveSpeed = swatProperties.crouchSpeed;
        if (hasJetPack && playerInput.jetPackFire)
        {
            float y = transform.position.y + 0.1f;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        Vector2 direction = new Vector2(playerInput.vertical * moveSpeed, playerInput.horizontal * moveSpeed);
        if (!GameManager.Instance.isSinglePlayer)
        {
            client.SendInformation("CMOV|" + c.clientId + "|" + direction.x.ToString() + "|" + direction.y.ToString() + "|" + transform.position.x.ToString()+"|"+transform.position.y.ToString()+"|"+transform.position.z.ToString());

            client.SendInformation("CPOS|" +c.clientId.ToString()+ "|" +transform.rotation.eulerAngles.x.ToString()+"|"+transform.rotation.eulerAngles.y.ToString()+"|"+transform.rotation.eulerAngles.z.ToString());
        }

        MoveController.SimpleMove(transform.forward * direction.x  + transform.right * direction.y);

        if (transform.position.y >0.3)
            return;

        if (Vector3.Distance(previousPosition, transform.position) > minimumMovetreshHold)
            footSteps.Play();
        previousPosition = transform.position;
    }
    void SpawnAtSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
        if(!GameManager.Instance.isSinglePlayer)
            c.Send("CPOS" + "|" + c.clientId + "|" + transform.position.x.ToString() + "|" + transform.position.y.ToString() + "|" + transform.position.z.ToString());
    }
}
