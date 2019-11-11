using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour {

	// Use this for initialization
	void Start () {//TRY - CATCH KOY

            
        SecondGameManager.Instance.Timer.Add(() => { 
        try
        {
                Destroy(transform.gameObject);
        }
        catch (MissingReferenceException)
        {
            return;
        }
            }, 3);
        

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
