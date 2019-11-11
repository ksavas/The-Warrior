using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

    [SerializeField] public SpawnPoints[] spawnPoints;
    [SerializeField] HealthCounter healthCounter;
    [SerializeField] GameObject ammo;
    [SerializeField] GameObject health;
    [SerializeField] GameObject jetPack;
    int selector;
    int spawnIndex;


    Client c;

    public bool jetPackIsDead = false;
    public bool healthGained = false;

    void Start()
    {
        if (!GameManager.Instance.isSinglePlayer)
        {
            c = FindObjectOfType<Client>();
            if (!c.isHost)
            {
                GetComponent<PickUpController>().enabled = false;
                return;
            }
        }
        spawnPoints = transform.Find("PickUpSpawnPointContainer").GetComponentsInChildren<SpawnPoints>();
        SecondGameManager.Instance.Timer.Add(() => { GenerateHealth(); }, Random.Range(1, 4));
        SecondGameManager.Instance.Timer.Add(() => { GenerateJetPack(); }, Random.Range(1, 4));

    }
    public void GenerateAmmo(string name)
    {/*
        spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject newAmmo = Instantiate(ammo);
        selector = Random.Range(0, 2);
        if (name == "PlayerVillager")
        {
            if(selector == 0)
            {
                newAmmo.name = "Shotgun";
                newAmmo.GetComponent<Ammo>().eWeaponType = EWeaponType.PISTOL;
            }
            else
            {
                newAmmo.name = "Revolver";
                newAmmo.GetComponent<Ammo>().eWeaponType = EWeaponType.PISTOL;
            }
        }
        else if (name == "PlayerAlien")
        {
            if (selector == 0)
            {
                newAmmo.name = "AlienRifle";
                newAmmo.GetComponent<Ammo>().eWeaponType = EWeaponType.PISTOL;
            }
            else
            {
                newAmmo.name = "AlienGun";
                newAmmo.GetComponent<Ammo>().eWeaponType = EWeaponType.PISTOL;
            }
        }
        else if (name == "PlayerSwat")
        {
            if (selector == 0)
            {
                newAmmo.name = "Rifle";
                newAmmo.GetComponent<Ammo>().eWeaponType = EWeaponType.PISTOL;
            }
            else
            {
                newAmmo.name = "M9";
                newAmmo.GetComponent<Ammo>().eWeaponType = EWeaponType.PISTOL;
            }
        }
        newAmmo.transform.position = spawnPoints[spawnIndex].transform.position;
        SecondGameManager.Instance.Timer.Add(() => { GenerateAmmo(name); }, Random.Range(200, 400));*/
    }

    public void GenerateHealth()
    {
        GameObject newHealth = Instantiate(health);
        spawnIndex = Random.Range(0, spawnPoints.Length);
        newHealth.name = "Health";
        newHealth.GetComponent<Health>().pickUpContainer = this;
        newHealth.GetComponent<Health>().counter = healthCounter;
        newHealth.transform.position = spawnPoints[spawnIndex].transform.position;
        if (!GameManager.Instance.isSinglePlayer)
            c.Send("HLT|" + newHealth.transform.position.x.ToString() + "|" + newHealth.transform.position.y.ToString() + "|" + newHealth.transform.position.z.ToString());
    }

    public void GenerateJetPack()
    {
        GameObject newJetpack = Instantiate(jetPack);
        spawnIndex = Random.Range(0, spawnPoints.Length);
        newJetpack.name = "Jet Pack";
        newJetpack.GetComponent<JetPack>().pickUpContainer = this;
        newJetpack.transform.position = spawnPoints[spawnIndex].transform.position;       
        if (!GameManager.Instance.isSinglePlayer)
            c.Send("JETP|" + newJetpack.transform.position.x.ToString() + "|" + newJetpack.transform.position.y.ToString() + "|" + newJetpack.transform.position.z.ToString());

    }
    public void DestroyHealth()
    {
        Destroy(FindObjectOfType<Health>().gameObject);
    }
    public void DestroyJetPack()
    {
        Destroy(FindObjectOfType<JetPack>().gameObject);
    }
    void Update()
    {
        if (!GameManager.Instance.isSinglePlayer)
        {
            c = FindObjectOfType<Client>();
            if (!c.isHost)
                return;
        }
        if (healthGained)
        {
            SecondGameManager.Instance.Timer.Add(() => 
            {

                GenerateHealth(); 

            }, Random.Range(1, 4));
            healthGained = false;
        }
        if(jetPackIsDead)
        {
            SecondGameManager.Instance.Timer.Add(() =>
            {

                GenerateJetPack(); 

            }, Random.Range(1, 4));
            jetPackIsDead = false;
        }
    }
}
