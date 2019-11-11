using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;
using System.Text;


public class Server : MonoBehaviour {
    public int port =6321;
    
    public List<ServerClient> clients; // private'dı
    private List<ServerClient> disconnectedList;

    private TcpListener server;
    private bool serverStarted;

    private void Update()
    {
      
        if (!serverStarted)
            return;
       
        foreach (ServerClient c in clients)
        {
            if (!IsConnected(c.tcp))
            {
                
                c.tcp.Close();
                disconnectedList.Add(c);
                continue;
            }
            else
            {
              
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)// BU BÖLÜMÜ data.lastchar == "<<"  diye değiştirebilirsin
                    {
                        OnIncomingData(c, data);
                    }

                }
            }
        }
        for (int i = 0; i < disconnectedList.Count - 1; i++)
        {

            //  TELL ALL PLAYERS WHO HAS DISCONNECTED

            clients.Remove(disconnectedList[i]);
            disconnectedList.RemoveAt(i);
        }
    }
    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        clients = new List<ServerClient>();
        disconnectedList = new List<ServerClient>();

        try
        {
            server = new TcpListener(IPAddress.Any,port);
            server.Start();
            StartListening();
            serverStarted = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: "+e.Message);

        }

    }
    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }
    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        string allUsers = "";
        foreach (ServerClient i in clients)
        {
            allUsers = i.clientName + '|'+","+i.clientChar.ToString();
        }

        ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(ar));
        clients.Add(sc);
        GameManager.Instance.mapCountChanged = true;
        sc.clientId = clients.IndexOf(sc);
        StartListening();
        Broadcast("SWHO|" + (clients.Count-1).ToString()+"|"+ allUsers, clients[clients.Count - 1]);
       
    }
    private bool IsConnected(TcpClient c)
    {
        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                return true;
            }
            else return false;
        }
        catch
        {
            return false;
        }
    }
    private void Broadcast(String data,List<ServerClient> cl)
    {
       
        foreach (ServerClient sc in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(sc.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
               
            }
            catch (Exception e)
            {
                Debug.Log("write error: "+ e.Message);
            }
        }
    }
    private void Broadcast(String data, ServerClient c)
    {
        List<ServerClient> sc = new List<ServerClient> {c};
        Broadcast(data, sc);
    }


    private void OnIncomingData(ServerClient c, string data)
    {

        string[] aData = data.Split('|');
        switch (aData[0])
        {
            case"CWHO":
                c.clientName = aData[1];
                c.clientIp = aData[2];
               // c.isHost = (aData[2]=="0") ? false : true;// pratik yapmak için (düzenlenecek)
                Broadcast("SCNN|"+c.clientName , clients);    // bütün clientlara yeni gelen client'ı tanıtıyor
                break;
            case"CUPT":
                Broadcast("SUPT|"+aData[1]+"|"+aData[2],clients);
                break;
            case"CFIRE":
                Broadcast("SFIRE|" + aData[1] + "|" + aData[2],clients);
                break;
            case"HLT":
                Broadcast("SHTLH|" + aData[1] + "|" + aData[2] + "|" + aData[3], clients);
                break;
            case"JETP":
                Broadcast("SJET|" + aData[1] + "|" + aData[2] + "|" + aData[3], clients);
                break;
            case"DHLT":
                Broadcast("SDHLT|", clients);
                break;
            case"DJET":
                Broadcast("SDJET|" , clients);
                break;
            case"CPOS":
                Broadcast("SPOS|"+ aData[1] + "|" + aData[2]+"|"+aData[3]+"|"+aData[4],clients);
                break;
            case"CDEAD":
                Broadcast("SDEAD|" + aData[1]+"|"+aData[2], clients);
                break;
            case"CDMG":
                Broadcast("SDMG|" + aData[2], clients[int.Parse(aData[1])]);
                break;
            case"CHUPT":
                Broadcast("SHUPT|" +aData[1] , clients);
                break;
            case"CFIN":
                Broadcast("SFIN|", clients);
                break;
            case"CWIN":
                print(aData[1]);
                Broadcast("SWIN|"+aData[1], clients);
                break;
        }
    }
    private void OnApplicationQuit()
    {
        CloseSocket();
    }
    private void OnDisable()
    {
        CloseSocket();
    }
    private void CloseSocket()
    {
        if (!serverStarted)
            return;
        server.Stop();
        serverStarted = false;
    }
} 

public class ServerClient
{
    public string clientName;
    public TcpClient tcp;
    public bool isHost;
    public int clientId;
    public int clientChar;
    public string clientIp;
    public ServerClient(TcpClient tcp)
    {
        this.tcp = tcp;
    }
}
