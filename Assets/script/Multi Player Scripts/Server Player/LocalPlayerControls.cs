using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerControls : MonoBehaviour {

    Client client;

	void Start () {
        client = GameObject.Find("Client(Clone)").GetComponent<Client>();
	}

    public void SendPosition(string position)
    {
        client.Send("CUPT|"+client.clientName+"|"+position);
    }
	
	void Update () {
		
	}
}
