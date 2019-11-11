using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serverPlayerContainer : MonoBehaviour {


    [SerializeField]Client client;
    ServerPlayerController[] controllers = new ServerPlayerController[4];
    int id;
    void Start()
    {
        id = 0;
        client = GameObject.Find("Client(Clone)").GetComponent<Client>();
        client.CreateServerPlayers(this);
        client.SetServerPlayerContainer(transform.gameObject);
    }

    public void CreateServerPlayer(GameClient c, int clientId)
    {
        if ((clientId - id) == 1)
        {
            Destroy(transform.GetChild(id).gameObject);
            id++;
        }

        transform.GetChild(id).GetComponent<ServerPlayerController>().enabled = true;
        transform.GetChild(id).GetComponent<ServerPlayerController>().GeneratePlayer(c);
        transform.GetChild(id).GetComponent<ServerPlayerController>().clientId = clientId.ToString();
        transform.GetChild(id).name = c.name;
        controllers[id] = transform.GetChild(id).GetComponent<ServerPlayerController>();
        id++;
    }
    public void MoveChildren(int id,float px, float py, float pz)//, float rx, float ry, float rz)
    {
        controllers[id].SpawnPlayer(px,py,pz);//,rx,ry,rz);
        controllers[id].spawned = true;

    }
    public void MChildren(int id, float dx, float dy, float x, float y, float z)
    {
        controllers[id].SetMovePoint(dx,dy,x,y,z);
    }
    public void Attack(int id, float x, float y, float z)
    {
        controllers[id].Attack(x, y, z);
    }

    public void SendSpawnPoint(int id ,float x, float y, float z)
    {
        controllers[id].SetSpawnPoint(x,y,z);
    }
    public void SendSpecDead(int deadId)
    {
        controllers[deadId].SetDead();
        controllers[deadId].SetDeadScore();
    }
    public void SendSpecKill(int killerId)
    {
        controllers[killerId].SetKillScore();
    }
    public void SendDead(int killerId, int deadId)
    {
        controllers[deadId].SetDead();
        controllers[deadId].SetDeadScore();
        controllers[killerId].SetKillScore();
    }
    public void Sendhealth(int id)
    {
        controllers[id].SetHealth();
    }
    public void DestroyChildren()
    {
        for (int i = id; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);

        }
    }
	
}
