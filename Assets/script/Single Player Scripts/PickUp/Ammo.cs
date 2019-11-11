using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : PickUpItem {

    [SerializeField] public EWeaponType eWeaponType;
    [SerializeField] float respawnTime;
    [SerializeField] int amount;
    Shooter shooter;

    void Start()
    {
        if (eWeaponType == EWeaponType.PISTOL)
        {
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.green;
        }
        else
        {
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.blue;
        }
    }

    public override void OnPickUpItem(Transform item)
    {
        shooter = item.GetComponent<Player>().PlayerShoot.ActiveWeapon as Shooter;
        var playerInventory = item.GetComponentInChildren<Container>();
        //SingleGameManager.Instance.Respawner.Despawn(gameObject, respawnTime);
        playerInventory.Put(transform.name, Random.Range(10,30));
        shooter.reloader.HandleOnAmmoChanged();
        Destroy(transform.gameObject);
        //amount = Random.Range(10, 30);
    }

}
