using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostClicker : MonoBehaviour {

    Color myColor = new Color();
    private bool selected = false;
    public static string hostAdress;
    public static int totalPlayerCount;
    void Start()
    {
        hostAdress = "";
    }
    public void Selected()
    {
        if (selected)
        {
            hostAdress = "";
            ColorUtility.TryParseHtmlString("#C0C0C064", out myColor);
            transform.gameObject.GetComponent<Image>().color = myColor;
            selected = false;
        }
        else
        {
            hostAdress = transform.name;

            string[] aData = transform.GetChild(1).transform.GetComponent<Text>().text.Split('/');
            totalPlayerCount = int.Parse(aData[1]);
            ColorUtility.TryParseHtmlString("#87858564", out myColor);
            transform.gameObject.GetComponent<Image>().color = myColor;
            selected = true;
        }
    }
}
