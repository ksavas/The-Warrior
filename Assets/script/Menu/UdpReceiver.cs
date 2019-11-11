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


public class UdpReceiver : MonoBehaviour {

    private UdpClient receiver;
    private bool transferStarted = false;
    private int remotePort = 19783;
    
    int lastProjId;

    Client c;
    GameObject serverPlayerContainer;
    serverPlayerContainer sPContainer;
    public void StartReceivingIP()
    {

        lastProjId = 0;
        DontDestroyOnLoad(transform.gameObject);
        try
        {
            if (receiver == null)
            {
                receiver = new UdpClient(remotePort);
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
        sPContainer = c.sPContainer;
    }

    private void ReceiveData(IAsyncResult result)
    {
        try
        {
            IPEndPoint receiveIPGroup = new IPEndPoint(IPAddress.Any, remotePort);
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
                case "SPOS":

                    if (bData[1] == c.clientId.ToString())
                        goto case "CALL";
                    if (sPContainer == null)
                        sPContainer = c.sPContainer;
                    sPContainer.MoveChildren(int.Parse(bData[1]), float.Parse(bData[2]), float.Parse(bData[3]), float.Parse(bData[4]));
                    goto case "CALL";
                
                case "SMOV":
                    if (bData[1] == c.clientId.ToString())
                        goto case "CALL";
                    if (sPContainer == null)
                        sPContainer = c.sPContainer;
                    sPContainer.MChildren(int.Parse(bData[1]), float.Parse(bData[2]), float.Parse(bData[3]), float.Parse(bData[4]), float.Parse(bData[5]), float.Parse(bData[6]));
                    goto case "CALL";

                default:
                    if (GameManager.Instance.hosts.Count > 0)
                    {
                        foreach (string s in GameManager.Instance.hosts)
                        {
                            string[] aData = s.Split('|');
                            if (aData[0] == bData[0] && aData[2] != bData[2])
                            {
                                GameManager.Instance.hosts.Remove(s);
                                GameManager.Instance.hosts.Add(receivedString);
                                receiver.BeginReceive(new AsyncCallback(ReceiveData), null);

                                return;
                            }
                        }
                    }
                    GameManager.Instance.hosts.Add(receivedString);
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


    public void CloseSocket()
    {
        if (transferStarted)
        {
            receiver.Close();
            transferStarted = false;
        }
            
    }
}
