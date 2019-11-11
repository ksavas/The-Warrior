using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerHealtCounter : MonoBehaviour {

    [SerializeField] Text text;
    void Start()
    {
        text.text = "100";
	}

    public void SetHitPoint(string hitPoint)
    {
        text.text = hitPoint;
    }
    public void SetHealth()
    {
        text.text = (int.Parse(text.text) + 25).ToString();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
