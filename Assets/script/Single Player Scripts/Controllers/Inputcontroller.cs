using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputcontroller : MonoBehaviour {


    public float vertical;
    public float horizontal;
    public Vector2 mouseInput;
    public bool fire1;
    public bool fire2;
    public bool reload;
    public bool isRunning;
    public bool isWalking;
    public bool isCrouching;
    public bool isSprinting;
    public bool mouseWheelUp;
    public bool mouseWheelDown;
    public bool scoreCounter;
    public bool jetPackFire;
    public bool escape;

	void Start () {
		
	}
	
	void Update () {

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        fire1 = Input.GetButton("Fire1");
        fire2 = Input.GetButton("Fire2");
        reload = Input.GetKey(KeyCode.R);
        isWalking = Input.GetKey(KeyCode.Z);
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isCrouching = Input.GetKey(KeyCode.C);
        isSprinting = Input.GetKey(KeyCode.V);
        mouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        mouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
        scoreCounter = Input.GetKey(KeyCode.Tab);
        jetPackFire = Input.GetKey(KeyCode.Space);
        escape = Input.GetKeyUp(KeyCode.Escape);

    }
}
