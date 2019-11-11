using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShoot : WeaponController {

    bool IsPlayerAlive ;
    Animator animator;

    UdpSender client;
    Client c;

    public bool knifeAttack;

    void Start()
    {
        IsPlayerAlive = true;
        GetComponent<Player>().PlayerHealth.OnDeath += PlayerHealth_OnDeath;
        if (!GameManager.Instance.isSinglePlayer)
        {
            client = FindObjectOfType<UdpSender>();
            c = FindObjectOfType<Client>();
        }
    }

    private void PlayerHealth_OnDeath()
    {
        IsPlayerAlive = false;
        ActiveWeapon.gameObject.SetActive(false);
        SecondGameManager.Instance.Timer.Add(() => { 
            IsPlayerAlive = true;
            ActiveWeapon.gameObject.SetActive(true);
        }, 2.95f);
    }

    void Update()
    {
        if (!IsPlayerAlive || GameManager.Instance.gameFinished)
            return;

        if (SecondGameManager.Instance.InputController.mouseWheelUp)
        {
            if(GameManager.Instance.isSinglePlayer)
                SwitchWeapon(1);
        }

        if (SecondGameManager.Instance.InputController.mouseWheelDown)
        {
            if (GameManager.Instance.isSinglePlayer)
                SwitchWeapon(-1);
        }
        if (SecondGameManager.Instance.LocaLPlayer.PlayerState.eMoveState == PlayerState.EMoveState.SPRINTING)
            return;

        if (!CanAttack)
            return;


        if (SecondGameManager.Instance.InputController.fire1)
        {
            if (ActiveWeapon is NonShooter)
            {
                knifeAttack = true;
                SecondGameManager.Instance.Timer.Add(() => { knifeAttack = false; }, 0.7f);
            }

            ActiveWeapon.Attack();

        }

        if (SecondGameManager.Instance.InputController.reload)
        {
            if (ActiveWeapon is NonShooter)
                return;
            else
                ((Shooter)ActiveWeapon).Reload();
        }
    }


}
