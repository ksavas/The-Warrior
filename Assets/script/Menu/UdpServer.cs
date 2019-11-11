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
using UnityEngine.UI;


public class UdpServer : MonoBehaviour {

    private UdpClient receiver;
    private bool transferStarted = false;
    private int remotePort = 19783;

    Client c;
    Server s;
    GameObject serverPlayerContainer;
    List<ServerClient> clients;
    public void StartReceivingIP()
    {
        DontDestroyOnLoad(transform.gameObject);
        try
        {
            if (receiver == null)
            {
                receiver = new UdpClient(19785);
                receiver.BeginReceive(new AsyncCallback(ReceiveData), null);
                transferStarted = true;
            }
        }
        catch (SocketException e)
        {
            Debug.Log(e.Message);
        }
    }
    public void SetClient(Client c)
    {
        this.c = c;
    }
    public void SetServer(Server s)
    {
        this.s = s;
    }
    private void ReceiveData(IAsyncResult result)
    {
        try
        {
            IPEndPoint receiveIPGroup = new IPEndPoint(IPAddress.Any, 19784);
            byte[] received;
            if (receiver != null)
            {
                received = receiver.EndReceive(result, ref receiveIPGroup);
            }
            else
            {
                return;
            }
            string receivedString = Encoding.ASCII.GetString(received);
            string[] bData = receivedString.Split('|');
            

            switch (bData[0])
            {
                case "CPOS":
                    SendData("SPOS|" + bData[1] + "|" + bData[2] + "|" + bData[3] + "|" + bData[4]);
                    goto case "CALL";
                
                case "CMOV":
                    SendData("SMOV|" + bData[1]+"|"+ bData[2]+ "|"+bData[3]+"|"+bData[4]+"|"+bData[5]+"|"+bData[6]);
                    goto case "CALL";

                case "CALL":
                    try
                    {
                        receiver.BeginReceive(new AsyncCallback(ReceiveData), null);
                    }
                    catch (Exception)
                    {
                        receiver.BeginReceive(new AsyncCallback(ReceiveData), null);
                    }
                    break;
            }
        }
        catch (Exception)
        {
            receiver.BeginReceive(new AsyncCallback(ReceiveData), null);
        }
    }

    private void SendData(string data)
    {
        clients = s.clients;
        foreach (ServerClient sc in clients)
        {
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse(sc.clientIp), remotePort);
            receiver.Send(Encoding.ASCII.GetBytes(data), data.Length, groupEP);
        }
    }

    public void CloseSocket()
    {
        if (transferStarted)
        {
            receiver.Close();
            transferStarted = false;
        }
            
    }
}
