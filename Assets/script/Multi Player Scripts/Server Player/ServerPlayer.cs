using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerPlayer : MonoBehaviour {


    float y = 0 , z = 0,rx = 0,ry = 0,rz = 0;
    public float x = 0;
    bool posUpdated;
    [SerializeField] public GameObject projectile;

    bool isMoving = false;

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


    float latestPositionTime;

    public Vector3 targetPosition;

    bool isSpawned = false;

    void Start()
    {
        MoveController.center = new Vector3(0, 1, 0);
    }

    public void SetPosRot(float rx, float ry, float rz)
    {
        this.rx = rx;
        this.ry = ry;
        this.rz = rz;
    }

    public void SetSpawnPoint(float x, float y, float z)
    {
        if (!isSpawned)
        {
            transform.position = new Vector3(x, y, z);
            isSpawned = true;
        }
        
    }
    public void SetDead()
    {

        SkinnedMeshRenderer[] meshes = transform.Find("Mesh").transform.GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].enabled = false;
        }
        SecondGameManager.Instance.Timer.Add(() => { SetAlive(); }, 3f);
            isSpawned = false;
    }
    public void SetAlive()
    {
        SkinnedMeshRenderer[] meshes = transform.Find("Mesh").transform.GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].enabled = true;
        }
        if (GetComponent<ServerPlayerHealth>() != null)
        GetComponent<ServerPlayerHealth>().isDead = false;
        GetComponent<ServerPlayerHealth>().Reset();

    }
    public void SetMovePoint(float dx, float dy, float x, float y, float z)
    {
        this.x = dx;
        this.y = dy;
        targetPosition = new Vector3(x, y, z);
    }
    void Update()
    {
        if (isSpawned)
        {
            Vector2 direction = new Vector2(x, y);
            MoveController.SimpleMove(transform.forward * direction.x + transform.right * direction.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }

        float angle = Mathf.LerpAngle(transform.eulerAngles.y, ry,5* Time.deltaTime);
        transform.eulerAngles = new Vector3(rx, angle, rz);
    }

}
