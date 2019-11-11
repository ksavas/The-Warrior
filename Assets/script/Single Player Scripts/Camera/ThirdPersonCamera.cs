using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    [System.Serializable]
    public class CameraRig
    {
        public Vector3 cameraOffset;
        public float damping;
        public float crouchHeight;
    }

    [SerializeField] CameraRig defaultCameraRig;
    [SerializeField] CameraRig aimCameraRig;

    [SerializeField] Transform cameraLookTarget;
    [SerializeField] Player localPlayer;

	void Awake () {
        SecondGameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;// BURAYI ÇÖZ !!!!
        HandleOnLocalPlayerJoined(localPlayer);
	}
    void HandleOnLocalPlayerJoined(Player player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("AimPivot");

        if (cameraLookTarget == null)
            cameraLookTarget = localPlayer.transform;

    }
	void LateUpdate () {

        CameraRig currentCamera = defaultCameraRig;

        if (localPlayer.PlayerState.eWeaponState == PlayerState.EWeaponState.AIMING || localPlayer.PlayerState.eWeaponState == PlayerState.EWeaponState.AIMEDFIRING)
            currentCamera = aimCameraRig;

        float targetHeight = currentCamera.cameraOffset.y + (localPlayer.PlayerState.eMoveState == PlayerState.EMoveState.CROUCHING ? currentCamera.crouchHeight : 0);

        Vector3 targetPosition = cameraLookTarget.position +
               localPlayer.transform.right * currentCamera.cameraOffset.x +
               localPlayer.transform.up * targetHeight +
               localPlayer.transform.forward * currentCamera.cameraOffset.z;

        transform.position = Vector3.Lerp(transform.position, targetPosition, currentCamera.damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraLookTarget.rotation , currentCamera.damping * Time.deltaTime); //targetRotation
	}   
}
