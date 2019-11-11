using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerPlayerAnimation : MonoBehaviour {

    Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
	}
	
	void Update () {


        animator.SetBool("isAiming", true);
        if (GetComponent<ServerPlayer>().x > 2.5 || GetComponent<ServerPlayer>().x < -2.5){
            if (GetComponent<ServerPlayer>().transform.position.y < 1)
                animator.SetFloat("Vertical", 1f);
        }
            
        else
            animator.SetFloat("Vertical", 0f);


	}
}
