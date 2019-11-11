using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using System.Text;
public class Client : MonoBehaviour {

    public string clientName;
    public int clientChar;
    public bool isHost;// BURASI AYARLANICAK "CLIENTID" VS OLABİLİR
    public int clientId=0;

    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;
    private bool gameStart = false;

    [SerializeField] public GameObject serverPlayerContainer;
    public serverPlayerContainer sPContainer;

    ServerPickUpController sPickUpController;
    PickUpController pickUpController;

    ServerHealtCounter sHealthCounter;

    float timeLeft = 3;

    public List<GameClient> players = new List<GameClient>(); // private'dı

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)// burdada "|" kontrolünü yapmalısın !!!!!
                {
                    OnIncomingData(data);   
                }
            }
        }
        if (gameStart)
        {
            timeLeft -= Time.deltaTime;
            int time = (int)timeLeft;
            GameManager.Instance.MultiGamePreparing("|"+time.ToString());
            if (timeLeft < 0)
            {
                GameManager.Instance.StartMultiPlayerGame();
                gameStart = false;
            }
        }


    }
    public bool ConnectToServer(string host, int port)
    {
        if (socketReady)
            return false;

        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
            //--
            GameManager.Instance.hostAddress = host;
            //--
        }
        catch(Exception e)
        {
            Debug.Log("socket error: " + e.Message);
           
        }
        return socketReady;
    }
    public void Send(string data)
    {
        if (!socketReady) 
            return;
        writer.WriteLine(data);
        writer.Flush();
    }

    private void OnIncomingData(String data)
    {
       // Debug.Log("client : "+data);
        
        string[] aData = data.Split('|');
        
        switch (aData[0])
        {
            case "SWHO":// Server'a yeni bağlandığımızda bağlı olan diğer clientların ve client idmizin bize aktarılması
                for (int i = 2; i < aData.Length-1; i++)
                {
                    UserConnected(aData[i], false);// CLIENT OYUNDAKİ BÜTÜN CLIENTLARI KENDİSİNE KAYDEDİYOR
                }
                clientId = int.Parse(aData[1]);
                clientChar = GameManager.Instance.currentcharacterIndex;
                Send("CWHO|" + clientName + "," + clientChar.ToString() + "|" + FindObjectOfType<UdpSender>().GetIpFromOutside());
                    break;
            case"SCNN"://   BU BÖLÜM EKRANA YAZDIRILICAK (BAĞLANANLARI GÖSTERMEK İÇİN) DÜZENLEME GEREKEBİLİR
                    UserConnected(aData[1], false);// HOST OLUP OLMADIĞINI BİLMİYORUZ FALSE BIRAKTIK BAKICAZ
                    break;
            case"SUPT":
                    if (aData[1] == clientId.ToString())
                        return;
                    string[] tData = aData[2].Split(',');
                    break;
            case"SFIRE":
                    if (aData[1] == clientId.ToString())
                        return;
                    string[] bulletData = aData[2].Split(',');
                    sPContainer.Attack(int.Parse(aData[1]), float.Parse(bulletData[0]), float.Parse(bulletData[1]), float.Parse(bulletData[2]));
                    break;
            case"SHTLH":
                    if (isHost)
                        return;
                    sPickUpController = FindObjectOfType<ServerPickUpController>();
                    sPickUpController.GenerateHealth(float.Parse(aData[1]),float.Parse(aData[2]),float.Parse(aData[3]));
                    break;
            case"SJET":
                    if (isHost)
                        return;
                    sPickUpController = FindObjectOfType<ServerPickUpController>();
                    sPickUpController.GenerateJetPack(float.Parse(aData[1]), float.Parse(aData[2]), float.Parse(aData[3]));
                    break;
            case"SDJET":
                    if(isHost)
                    {
                        pickUpController = FindObjectOfType<PickUpController>();
                        pickUpController.DestroyJetPack();
                        pickUpController.jetPackIsDead = true;
                        return;
                    }
                    sPickUpController = FindObjectOfType<ServerPickUpController>();
                    sPickUpController.DestroyJetPack();
                    break;
            case "SDHLT":
                    if(isHost)
                    {
                       // pickUpController = FindObjectOfType<PickUpController>();
                        //pickUpController.DestroyHealth();
                       // pickUpController.healthGained = true;
                        return;
                    }
                    print("fdgdfs");
                    sPickUpController = FindObjectOfType<ServerPickUpController>();
                    sPickUpController.DestroyHealth();
                    break;
            case"SPOS":
                    if (aData[1] == clientId.ToString())
                        return;
                    sPContainer.SendSpawnPoint(int.Parse(aData[1]),float.Parse(aData[2]),float.Parse(aData[3]),float.Parse(aData[4]));
                    break;
            case"SDEAD":
                    if (aData[2] == clientId.ToString())
                    {
                        FindObjectOfType<PlayerHealth>().Die();
                        FindObjectOfType<PlayerController>().SetDeadScore();
                        sPContainer.SendSpecKill(int.Parse(aData[1]));
                        return;
                    }
                    if (aData[1] == clientId.ToString())
                    {
                        FindObjectOfType<PlayerController>().SetKillScore();
                        sPContainer.SendSpecDead(int.Parse(aData[2]));
                        return;
                    }
                    sPContainer.SendDead(int.Parse(aData[1]), int.Parse(aData[2]));
                    break;
            case"SDMG":
                    sHealthCounter = FindObjectOfType<ServerHealtCounter>();
                    sHealthCounter.SetHitPoint(aData[1]);
                    FindObjectOfType<PlayerHealth>().hitPointsRemaining = int.Parse(aData[1]);
                    break;
            case"SHUPT":
                    if (aData[1] == clientId.ToString())
                        return;
                    sPContainer.Sendhealth(int.Parse(aData[1]));
                    sPickUpController = FindObjectOfType<ServerPickUpController>();
                    sPickUpController.DestroyHealth();
                    break;
            case"SFIN":
                    GameManager.Instance.gameFinished = true;
                    SecondGameManager.Instance.Timer.Add(() => { Application.Quit(); }, 3);
                    break;
            case"SWIN":
                    FindObjectOfType<WinnerShower>().SetWinner(aData[1]);
                    break;
        }

    }
    private void UserConnected(string definition,bool host)// BURADA CLIENTIN SADECE NAME BİLGİSİ VAR DÜZENLEME GEREKİCEK
    {
        string[] lData = definition.Split(',');
        GameClient c = new GameClient();
        c.name = lData[0];
        c.clientChar = int.Parse(lData[1]);
        players.Add(c);
        c.clientId = players.IndexOf(c);
        GameManager.Instance.MultiGamePreparing(lData[0]);
        if (players.Count == GameManager.Instance.totalPlayerCount)
            gameStart = true;
    }

    public void CreateServerPlayers(serverPlayerContainer sPContainer)
    {
        FindObjectOfType<UdpReceiver>().SetClient(this);
        this.sPContainer = sPContainer; //GameObject.Find("ServerPlayerContainer").GetComponent<serverPlayerContainer>();
        foreach (GameClient c in players)
        {
            if (c.clientId == clientId)
                continue;
            sPContainer.CreateServerPlayer(c,c.clientId);
        }
        sPContainer.DestroyChildren();

    }

    public void SetClientCharacter(int charIndex)
    {
        clientChar = charIndex;
    }

    public void SetServerPlayerContainer(GameObject sp)
    {
        serverPlayerContainer = sp;
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
        if (!socketReady)
            return;
        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }
}

public class GameClient
{
    public string name;
    public bool isHost;
    public int clientId;
    public int clientChar;
}