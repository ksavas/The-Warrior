using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public enum EMoveState
    {
        RUNNING,
        WALKING,
        CROUCHING,
        SPRINTING
    }

    public enum EWeaponState
    {
        IDLE,
        FIRING,
        AIMING,
        AIMEDFIRING
    }

    public EMoveState eMoveState;
    public EWeaponState eWeaponState;

    private Inputcontroller m_InputController;
    public Inputcontroller inputController
    {
        get
        {
            if (m_InputController == null)
                m_InputController = SecondGameManager.Instance.InputController;
            return m_InputController;
        }
    }
    void Update()
    {
        SetMoveState();
        SetWeaponState();
    }
    void SetMoveState()
    {
        eMoveState = EMoveState.RUNNING;
        if (inputController.isSprinting)
            eMoveState = EMoveState.SPRINTING;
        if (inputController.isWalking)
            eMoveState = EMoveState.WALKING;
        if (inputController.isCrouching)
            eMoveState = EMoveState.CROUCHING;
    }
    void SetWeaponState()
    {
        eWeaponState = EWeaponState.IDLE;
        if (inputController.fire1)
            eWeaponState = EWeaponState.FIRING;
        if (inputController.fire2)
            eWeaponState = EWeaponState.AIMING;
        if (inputController.fire1 && inputController.fire2)
            eWeaponState = EWeaponState.AIMEDFIRING;
    }    
}
