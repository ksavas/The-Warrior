using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPackCounter : MonoBehaviour {

    [SerializeField] Text text;
    public Player player;
    JetPack jetPack;

    void Start () {
		
	}
    void Update () {
        if (player == null)
            return;
        if(!player.hasJetPack)
        {
            text.text = "0";
            return;
        }

        jetPack = player.GetComponentInChildren<JetPack>();
        text.text = jetPack.gasRemaining.ToString();
	}
}
