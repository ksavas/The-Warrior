using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    Animator animator;
    private PlayerAim m_PlayerAim;
    private PlayerAim playerAim
    {
        get
        {
            if (m_PlayerAim == null)
                m_PlayerAim = SecondGameManager.Instance.LocaLPlayer.playerAim;
            return m_PlayerAim;
        }
    }

	void Awake() {

        animator = GetComponentInChildren<Animator>();

	}
	
	void Update () {

        animator.SetFloat("Vertical", SecondGameManager.Instance.InputController.vertical);
        animator.SetFloat("Horizontal", SecondGameManager.Instance.InputController.horizontal);
        animator.SetBool("isWalking", SecondGameManager.Instance.InputController.isWalking);
        animator.SetBool("isSprinting", SecondGameManager.Instance.InputController.isSprinting);
        animator.SetBool("isCrouching", SecondGameManager.Instance.InputController.isCrouching);
        animator.SetFloat("AimAngle",playerAim.GetAngle());
        animator.SetBool("isAiming", SecondGameManager.Instance.LocaLPlayer.PlayerState.eWeaponState == PlayerState.EWeaponState.AIMING ||
        SecondGameManager.Instance.LocaLPlayer.PlayerState.eWeaponState == PlayerState.EWeaponState.AIMEDFIRING);
        animator.SetBool("hasKnife", SecondGameManager.Instance.LocaLPlayer.PlayerShoot.ActiveWeapon.eWeaponType  == EWeaponType.NONSHOOT);
        animator.SetBool("isKnifeAttack", SecondGameManager.Instance.LocaLPlayer.PlayerShoot.knifeAttack);
        animator.SetBool("hasJetPack", SecondGameManager.Instance.InputController.jetPackFire && SecondGameManager.Instance.LocaLPlayer.hasJetPack);

	}
}
