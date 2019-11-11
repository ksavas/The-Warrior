using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System;

public class UdpSender : MonoBehaviour {

    private UdpClient sender;

    private int remotePort = 19783;
    private int localPort = 19784;

    private string hostName;
    private string data;
    public string ipAdress;
    private string hostMapName;
    private int playerCount;
    public int currentPlayerCount=0;
    private bool transferStarted = false;
    public void Init(string name, string map, int count)
    {
        DontDestroyOnLoad(transform.gameObject);
        this.hostName = name;
        this.hostMapName = map;
        this.playerCount = count;
        try
        {
            sender = new UdpClient(localPort, AddressFamily.InterNetwork);
            if (count == 0)
                return;
            sender.EnableBroadcast = true;
            transferStarted = true;
            SendData();
            InvokeRepeating("SendData", 0, 2f);

            
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
        }
    } 
    private void SendData()
    {
        if (GameManager.Instance.multiPlayerGameStarted)
        {
            CancelInvoke("SendData");
            return;
        }
        //IPAddress.Parse(aData[0] + "." + aData[1] + "." +aData[2] +".255")
        data = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
        if (data != "")
        {
            string[] aData = data.Split('.');
            data = data + "|" + hostName + "|" + currentPlayerCount.ToString() + "/" + playerCount.ToString() + "|" + hostMapName;
            for (int i = 2; i < 15; i++)
            {
                IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse(aData[0] + "." + aData[1] + "." +aData[2] +"."+i.ToString()), remotePort);
                sender.Send(Encoding.ASCII.GetBytes(data), data.Length, groupEP);
                print("sdasdsadsa");
         
            }


        }
        else
        {
            
        }
    }

    public void SendInformation(string data)
    {
        string ipAddress = GameManager.Instance.hostAddress;
        if(ipAddress == "127.0.0.1")
            ipAddress = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
        IPAddress ipAdd;
        try
        {
             ipAdd = IPAddress.Parse(ipAddress);
             IPEndPoint groupEP = new IPEndPoint(ipAdd, 19785);
             sender.Send(Encoding.ASCII.GetBytes(data), data.Length, groupEP);
        }
        catch (Exception e)
        {
            throw e;
        }
        


    }

    public string GetLocalIPv4(NetworkInterfaceType _type)
    {
        try
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                            return output;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            GetLocalIPv4(NetworkInterfaceType.Wireless80211);
        }
        return "";
    }
    public string GetIpFromOutside()
    {
        return GetLocalIPv4(NetworkInterfaceType.Wireless80211);
    }
    public void CloseSocket()
    {
        if (transferStarted)
        {
            sender.Close();
            transferStarted = false;
        }
            
    }

}
