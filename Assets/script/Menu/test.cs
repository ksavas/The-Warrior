using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {

    public static test Instance { set; get; }


    public Transform messageContianer;
    public GameObject messagePrefab;
    public GameObject tst;
    public GameObject ts;
    public Client client;

    float a = 0;
    void Start()
    {
        Instance = this;
        client = FindObjectOfType<Client>();
    }

    public void TextUpdate(string b,int id)// gelen bilgi her client için ayrı ayrı olup ayrı şekilde tutulmalı
                                           // textUpdate methoduna her client için overload yapabilirsin.
    {
        Debug.Log("gelen: " + id.ToString() + "bizdeki" + client.clientId.ToString());
        a = float.Parse(b); // int.Parse(b);
        if (id==0)
        {
            tst.gameObject.transform.position = new Vector3(tst.transform.position.x, a * .1f, tst.transform.position.z);
        }
        else if(id == 1)
        {
            ts.gameObject.transform.position = new Vector3(ts.transform.position.x, a * .1f, ts.transform.position.z);
        }
    }
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow))
        {
            a++;
            client.Send("CUPT|" +client.clientId.ToString()+"|"+a.ToString()+"|");
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            a--;
            client.Send("CUPT|"+client.clientId.ToString()+"|"+a.ToString()+"|");
        }
	}
    public void ChatMessage(string message)
    {
        GameObject go = Instantiate(messagePrefab) as GameObject ;
        go.transform.SetParent(messageContianer);
        go.transform.GetComponentInChildren<Text>().text = message;
    }
    public void SendChatMessage()
    {
        InputField i = GameObject.Find("MessageInput").GetComponent<InputField>();

        if (i.text == "")
            return;

        client.Send("CMSG|" + i.text);
        i.text = ""; 
    }
}
