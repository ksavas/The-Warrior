using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerPlayerController : MonoBehaviour {

    public int killScore = 0;
    public int deadScore = 0;

    GameClient attachedClient;
    GameObject currentPlayer;

    public float posX = 0;
    public float posY = 0;
    public float posZ = 0;
    public float rotX = 0;
    public float rotY = 0;
    public float rotZ = 0;

    public string clientId;

    ServerPlayer player;
    bool newBullet;
    public bool spawned;

	void Start () {
        player = GetComponentInChildren<ServerPlayer>();

	}
    public void GeneratePlayer(GameClient c)
    {
        attachedClient = c;
        if (attachedClient.clientChar == 0)
        {
            currentPlayer = Instantiate(Resources.Load("ServerPlayerAlien")) as GameObject;
        }
        else if (attachedClient.clientChar == 1)
        {
            currentPlayer = Instantiate(Resources.Load("ServerPlayerSwat")) as GameObject;
        }
        else if (attachedClient.clientChar == 2)
        {
            currentPlayer = Instantiate(Resources.Load("ServerPlayerVillager")) as GameObject;
        }

        currentPlayer.transform.parent = transform;
        currentPlayer.transform.name = attachedClient.name;
    }

    public void SpawnPlayer(float mx,float my,float mz)//, float rx, float ry, float rz)
    {
        player.SetPosRot(mx, my, mz);//, rx, ry, rz);
    }
    public void SetSpawnPoint(float x, float y, float z)
    {
        player.SetSpawnPoint(x, y, z);
    }
    public void SetMovePoint(float dx, float dy, float x, float y, float z)
    {
        player.SetMovePoint(dx,dy,x,y,z);
    }
    public void SetKillScore()
    {
        killScore++;
        FindObjectOfType<ScoreCounter>().UpdateKillScore(int.Parse(clientId),killScore);
    }
    public void SetDeadScore()
    {
        deadScore++;
        FindObjectOfType<ScoreCounter>().UpdateDeadScore(int.Parse(clientId), deadScore);
    }
    public void SetDead()
    {
        player.SetDead();
        
    }

    public void SetHealth()
    {
        player.GetComponent<ServerPlayerHealth>().HealthTaken();
    }

    public void Attack(float x, float y, float z)
    {
        var projectile = Instantiate(player.projectile);
        projectile.transform.parent = transform.GetChild(0).transform;
        projectile.transform.position = new Vector3(x, y, z);
        projectile.transform.localRotation = Quaternion.Euler(transform.parent.transform.eulerAngles.x, transform.parent.transform.eulerAngles.y *-1f , transform.parent.transform.eulerAngles.z);
    }
	// Update is called once per frame
	void Update () {

        if (spawned)
            return;
        transform.position = new Vector3(posX, posY, posZ);
        transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

	}
}
