using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServerPickUpController : MonoBehaviour {

    [SerializeField] HealthCounter healthCounter;
    [SerializeField] GameObject health;
    [SerializeField] GameObject jetPack;
    Client c;

    void Start()
    {
        if (!GameManager.Instance.isSinglePlayer)
        {
            c = FindObjectOfType<Client>();
            if (c.isHost)
            {
                GetComponent<ServerPickUpController>().enabled = false;
                return;
            }
                
        }
	}

    public void GenerateHealth(float x, float y, float z)
    {
        GameObject newHealth = Instantiate(health);
        newHealth.name = "SHealth";
        newHealth.GetComponent<Health>().sPickUpController = this;
        newHealth.GetComponent<Health>().counter = healthCounter;
        newHealth.transform.position = new Vector3(x, y, z);
    }

    public void GenerateJetPack(float x, float y, float z)
    {
        GameObject newJetpack = Instantiate(jetPack);
        newJetpack.name = "SJet Pack";
        newJetpack.GetComponent<JetPack>().sPickUpController = this;
        newJetpack.transform.position = new Vector3(x, y, z);
    }
    public void SendDestroyHealth()
    {
        print("safasdfsd");
        c.Send("DHLT|a");
    }
    public void SendDestroyJetPack()
    {
        c.Send("DJET|a");
    }
    public void DestroyHealth()
    {
        try
        {
            Destroy(FindObjectOfType<Health>().gameObject);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public void DestroyJetPack()
    {
        Destroy(FindObjectOfType<JetPack>().gameObject);
    }
	void Update () {

		
	}
}
