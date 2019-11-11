using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;
using System.Net.Sockets;




public class GameManager : MonoBehaviour {
    public enum Difficulty
    {
        NONE,
        EASY,
        MEDIUM,
        HARD
    }

    public string hostAddress;

    public int currentcharacterIndex;
    public int singlePlayerEnemyCount;
    public Difficulty difficulty;

    public bool isSinglePlayer;

    public bool multiPlayerGameStarted = false;

    public static GameManager Instance { set; get; }

    [SerializeField] GameObject easySelectButton;
    [SerializeField] GameObject mediumSelectButton;
    [SerializeField] GameObject hardSelectButton;

    public bool gameFinished = false;

    public GameObject mainMenu;
    public GameObject singlePlayerMenu;
    public GameObject multiPlayerMenu;
    public GameObject optionsMenu;
    public GameObject joinGamePanel;
    public GameObject createGamePanel;
    public GameObject charDefnPanel;
    public GameObject multiStartPanel;
    public GameObject mapDefnPanel;
    public GameObject backMainBtn;
    public GameObject backMultiBtn;
    public GameObject backToCreateBtn;
    public GameObject backToJoinBtn;
    public GameObject hostListPanel;
    public GameObject hostPrefab;
    public GameObject AlertDialogBox;
    public GameObject joinedHost;

    public GameObject serverPrefab;
    public GameObject clientPrefab;
    public GameObject udpSenderPrefab;
    public GameObject udpReceiverPrefab;
    public GameObject udpServerPrefab;

    public bool mapCountChanged = false;
    public int totalPlayerCount;
    public string singlePlayerName;
    public Transform hostContainer;

    LocalizedStrings localStrings;
    


    public HashSet<string> hosts = new HashSet<string>();

    private void Start()
    {
        localStrings = LocalizedStrings.m_LocalizedStrings;
        UpdateLanguage();
        Instance = this;
        currentcharacterIndex = 0;
        DontDestroyOnLoad(gameObject);
        mainMenu.SetActive(true);
        singlePlayerMenu.SetActive(false);
        multiPlayerMenu.SetActive(false);
        optionsMenu.SetActive(false);
        joinGamePanel.SetActive(false);
        createGamePanel.SetActive(false);
        charDefnPanel.SetActive(false);
        multiStartPanel.SetActive(false);
        mapDefnPanel.SetActive(false);
        backMainBtn.SetActive(false);
        backMultiBtn.SetActive(false);
        backToCreateBtn.SetActive(false);
        backToJoinBtn.SetActive(false);
        hostListPanel.SetActive(false);
        AlertDialogBox.SetActive(false);
    }
    public void SinglePlayerButton()
    {
        mainMenu.SetActive(false);
        singlePlayerMenu.SetActive(true);
        charDefnPanel.SetActive(true);
        mapDefnPanel.SetActive(true);
        backMainBtn.SetActive(true);
    }
    public void MultiplayerButton()
    {
        mainMenu.SetActive(false);
        joinGamePanel.SetActive(false);
        createGamePanel.SetActive(false);
        multiPlayerMenu.SetActive(true);
        backMainBtn.SetActive(true);
    }
    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        backMainBtn.SetActive(true);
    }
    public void JoinGameButton()
    {
        multiPlayerMenu.SetActive(false);
        joinGamePanel.SetActive(true);
        charDefnPanel.SetActive(true);
        hostListPanel.SetActive(true);
        backMainBtn.SetActive(false);
        backMultiBtn.SetActive(true);
        UdpReceiver ur = Instantiate(udpReceiverPrefab).GetComponent<UdpReceiver>();
        ur.StartReceivingIP();

        UdpSender us = Instantiate(udpSenderPrefab).GetComponent<UdpSender>();
        us.Init("", "",0);

        CreateHostTable();
    }
    public void RefreshHostTable()
    {
        CreateHostTable();
    }
    public void CreateGameButton()
    {
        multiPlayerMenu.SetActive(false);
        charDefnPanel.SetActive(true);
        mapDefnPanel.SetActive(true);
        createGamePanel.SetActive(true);
        backMainBtn.SetActive(false);
        backMultiBtn.SetActive(true);
    }
    public void CreateGameStartButton()
    {
        string hostname = charDefnPanel.transform.Find("PlayerNameInput").transform.Find("Text").GetComponent<Text>().text;
        int playercount = 0;
        string mapName = "Default";
        if (hostname == "")
        {
            AlertDialogBox.transform.Find("Text").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.NAME_ALERT);
            AlertDialogBox.SetActive(true);
            return;
        }
        if (mapDefnPanel.transform.Find("CountInput").transform.Find("Text").GetComponent<Text>().text == "")
        {
            AlertDialogBox.transform.Find("Text").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.COUNT_ALERT);
            AlertDialogBox.SetActive(true);
            return;
        }
        playercount = int.Parse(mapDefnPanel.transform.Find("CountInput").transform.Find("Text").GetComponent<Text>().text);
        if (playercount < 2 || playercount > 5)
        {
            AlertDialogBox.transform.Find("Text").GetComponent<Text>().text = string.Format(localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.COUNT_RANGER_ALERT_1) + "{0}" + localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.COUNT_RANGE_ALERT_2), Environment.NewLine);
            AlertDialogBox.SetActive(true);
            return;
        }
        charDefnPanel.SetActive(false);
        mapDefnPanel.SetActive(false);
        createGamePanel.SetActive(false);
        backMultiBtn.SetActive(false);
        multiStartPanel.SetActive(true);
        backToCreateBtn.SetActive(true);
        totalPlayerCount = playercount;
        UdpSender us = Instantiate(udpSenderPrefab).GetComponent<UdpSender>();
        isSinglePlayer = false;

        UdpServer uServer = Instantiate(udpServerPrefab).GetComponent<UdpServer>();
        uServer.StartReceivingIP();

        UdpReceiver ur = Instantiate(udpReceiverPrefab).GetComponent<UdpReceiver>();
        ur.StartReceivingIP();


        us.Init(hostname, mapName, playercount);
        try
        { 
            Server s = Instantiate(serverPrefab).GetComponent<Server>();
            uServer.SetServer(s);
            s.Init();
            Client c = Instantiate(clientPrefab).GetComponent<Client>();// Host eden kişide aynı zamanda client ve oyuncu
            uServer.SetClient(c);
            c.clientName = hostname;
            c.SetClientCharacter(currentcharacterIndex);
            c.isHost = true;
            c.ConnectToServer("127.0.0.1", 6321);// BURDAKİ 6321 PORT DEĞİŞKENİ OLARAK DEĞİŞSİN 
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }     
    }
    public void EasySelection()
    {
        difficulty = Difficulty.EASY;
        mapDefnPanel.transform.Find("DifficultyLabel").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.DIFFICULTY_LABEL) + localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.EASY_SELECTION);
    }
    public void MediumSelection()
    {
        difficulty = Difficulty.MEDIUM;
        mapDefnPanel.transform.Find("DifficultyLabel").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.DIFFICULTY_LABEL) + localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.MEDIUM_SELECTION);
    }
    public void HardSelection()
    {
        difficulty = Difficulty.HARD;
        mapDefnPanel.transform.Find("DifficultyLabel").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.DIFFICULTY_LABEL) + localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.HARD_SELECTION);
    }
    public void SingleGameStartButton()
    {
        isSinglePlayer = true;
        singlePlayerName = charDefnPanel.transform.Find("PlayerNameInput").transform.Find("Text").GetComponent<Text>().text;
        int playercount = 0;
        string mapName = "Default";
        if (singlePlayerName == "")
        {
            AlertDialogBox.transform.Find("Text").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.NAME_ALERT);
            AlertDialogBox.SetActive(true);
            return;
        }
        if (mapDefnPanel.transform.Find("CountInput").transform.Find("Text").GetComponent<Text>().text == "")
        {
            AlertDialogBox.transform.Find("Text").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.COUNT_ALERT);
            AlertDialogBox.SetActive(true);
            return;
        }
        playercount = int.Parse(mapDefnPanel.transform.Find("CountInput").transform.Find("Text").GetComponent<Text>().text);
        if (playercount < 2 || playercount > 4)
        {
            AlertDialogBox.transform.Find("Text").GetComponent<Text>().text = string.Format(localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.COUNT_RANGER_ALERT_1) + "{0}" + localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.COUNT_RANGE_ALERT_2), Environment.NewLine);
            AlertDialogBox.SetActive(true);
            return;
        }
        if (difficulty == Difficulty.NONE)
        {
            AlertDialogBox.transform.Find("Text").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.DIFFICULTY_ALERT);
            AlertDialogBox.SetActive(true);
            return;
        }
        singlePlayerEnemyCount = playercount;

        SceneManager.LoadScene("SinglePlayerGame");
        
    }
    public void joinGameStartButton()
    {
        isSinglePlayer = false;
        string clientName = charDefnPanel.transform.Find("PlayerNameInput").transform.Find("Text").GetComponent<Text>().text;
        if (clientName == "")
        {
            AlertDialogBox.transform.Find("Text").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.NAME_ALERT);
            AlertDialogBox.SetActive(true);
            return;
        }
        string hostAdress = HostClicker.hostAdress;
        if (hostAdress == "" || hostAdress==null)
        {
            AlertDialogBox.transform.Find("Text").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.GAME_SELECT_ALERT);
            AlertDialogBox.SetActive(true);
            return;
        }
        totalPlayerCount = HostClicker.totalPlayerCount;
        joinGamePanel.SetActive(false);
        charDefnPanel.SetActive(false);
        hostListPanel.SetActive(false);
        backMultiBtn.SetActive(false);
        multiStartPanel.SetActive(true);
        backToJoinBtn.SetActive(true);
        hostAddress = hostAdress;
        try
        {
            Client c = Instantiate(clientPrefab).GetComponent<Client>();
            c.isHost = false;
            c.clientName = clientName;
            c.ConnectToServer(hostAdress, 6321);// BURDAKİ 6321 PORT DEĞİŞKENİ OLARAK DEĞİŞSİN
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
    }
    public void CreateHostTable()
    {
        if (hostContainer.childCount > 0)
        {
            for (int i = 0; i < hostContainer.transform.childCount; i++)
            {
                var child = hostContainer.GetChild(i).gameObject;
                Destroy(child.gameObject);
            }
        }
        if (hosts.Count > 0)
        {
            foreach (string s in hosts)
            {
                string[] aData = s.Split('|');

                GameObject go = Instantiate(hostPrefab) as GameObject;
                go.transform.SetParent(hostContainer);
                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.transform.name = aData[0];
                go.transform.Find("NameLabel").GetComponent<Text>().text = aData[1];
                go.transform.Find("CountPanel").GetComponent<Text>().text = aData[2];
                go.transform.Find("MapPanel").GetComponent<Text>().text = aData[3];
            }
        }
        hosts.Clear();
    }
    public void MultiGamePreparing(string name)
    {

        if (name.StartsWith("|"))
        {
            if (multiStartPanel.transform.GetChild(multiStartPanel.transform.childCount - 1).name != "countDown")
            {
                GameObject go = Instantiate(joinedHost) as GameObject;
                go.GetComponent<Text>().text = name.Substring(1, name.Length - 1);
                go.transform.name = "countDown";
                go.transform.SetParent(multiStartPanel.transform);
                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            }
            else
            {

                multiStartPanel.transform.Find("countDown").GetComponent<Text>().text = name.Substring(1, name.Length - 1);
            }
        }

        else
        {
            GameObject go = Instantiate(joinedHost) as GameObject;
            go.GetComponent<Text>().text = name + localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.JOINED_ALERT);;
            go.transform.SetParent(multiStartPanel.transform);
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

    }
    public void MultiGameRemoving()
    {
        if (multiStartPanel.transform.childCount > 0)
        {
            for (int i = 0; i < multiStartPanel.transform.childCount; i++)
            {
                var child = multiStartPanel.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
    public void AlertOkButton()
    {
        AlertDialogBox.SetActive(false);
    }
    public void BackToMainButton()
    {
        mainMenu.SetActive(true);
        singlePlayerMenu.SetActive(false);
        charDefnPanel.SetActive(false);
        mapDefnPanel.SetActive(false);
        backMainBtn.SetActive(false);
        multiPlayerMenu.SetActive(false);
        backMainBtn.SetActive(false);
        optionsMenu.SetActive(false);


    }
    public void BackToMultiButton()
    {
        charDefnPanel.SetActive(false);
        mapDefnPanel.SetActive(false);
        createGamePanel.SetActive(false);
        backMultiBtn.SetActive(false);
        hostListPanel.SetActive(false);
        CloseSockets();
        MultiplayerButton();
    }
    public void BackToCreateGameButton()
    {
        CloseSockets();
        multiStartPanel.SetActive(false);
        backToCreateBtn.SetActive(false);
        MultiGameRemoving();
        CreateGameButton();
    }
    public void BackToJoinGameButton()
    {
        CloseSockets();
        multiStartPanel.SetActive(false);
        backToJoinBtn.SetActive(false);
        MultiGameRemoving();
        JoinGameButton();
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    void OnApplicationQuit() 
    {
        CloseSockets();
    }
    public void CloseSockets()
    {
        Server s = FindObjectOfType<Server>();
        if (s != null)
            Destroy(s.gameObject);

        Client c = FindObjectOfType<Client>();
        if (c != null)
            Destroy(c.gameObject);

        UdpSender us = FindObjectOfType<UdpSender>();
        if (us != null)
        {
            us.CloseSocket();
            Destroy(us.gameObject);
        }

        UdpReceiver ur = FindObjectOfType<UdpReceiver>();
        if (ur != null)
        {
            ur.CloseSocket();
            Destroy(ur.gameObject);
        }
        UdpServer uServer = FindObjectOfType<UdpServer>();
        if (uServer != null)
        {
            uServer.CloseSocket();
            Destroy(uServer.gameObject);
        }
            

    }
    public void StartMultiPlayerGame()
    {
        UdpSender us = FindObjectOfType<UdpSender>();
        //us.CancelInvoke();
        multiPlayerGameStarted = true;
        SceneManager.LoadScene("MultiPlayerGame");
    }
    public void NextCharButton()
    {
        charDefnPanel.transform.Find("CharacterHolder").transform.GetChild(currentcharacterIndex).gameObject.SetActive(false);
        currentcharacterIndex++;
        if (currentcharacterIndex >= charDefnPanel.transform.Find("CharacterHolder").transform.childCount-1)
            currentcharacterIndex = 0;
        charDefnPanel.transform.Find("CharacterHolder").transform.GetChild(currentcharacterIndex).gameObject.SetActive(true);
        CharSelectionName();
    }
    public void PreviousCharButton()
    {
        charDefnPanel.transform.Find("CharacterHolder").transform.GetChild(currentcharacterIndex).gameObject.SetActive(false);
        currentcharacterIndex--;
        if (currentcharacterIndex < 0)
            currentcharacterIndex = charDefnPanel.transform.Find("CharacterHolder").transform.childCount -2;
        charDefnPanel.transform.Find("CharacterHolder").transform.GetChild(currentcharacterIndex).gameObject.SetActive(true);
        CharSelectionName();
        //charDefnPanel.transform.FindChild("CharacterHolder").transform.FindChild("PlayerTitle").GetComponent<Text>().text = charDefnPanel.transform.FindChild("CharacterHolder").transform.GetChild(currentcharacterIndex).transform.name;
    }
    void CharSelectionName()
    {
        if (charDefnPanel.transform.Find("CharacterHolder").transform.GetChild(currentcharacterIndex).transform.name == "Alien")
            charDefnPanel.transform.Find("CharacterHolder").transform.Find("PlayerTitle").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.PLAYER_ALIEN);
        else if (charDefnPanel.transform.Find("CharacterHolder").transform.GetChild(currentcharacterIndex).transform.name == "Swat")
            charDefnPanel.transform.Find("CharacterHolder").transform.Find("PlayerTitle").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.PLAYER_SWAT);
        else
            charDefnPanel.transform.Find("CharacterHolder").transform.Find("PlayerTitle").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.PLAYER_VILLAGER);
    }
    public void SetLanguageTr()
    {
        PlayerPrefs.SetString("Language", "Tr");
        localStrings.CreateTurkishStrings();
        UpdateLanguage();
    }
    public void SetLanguageEng()
    {
        PlayerPrefs.SetString("Language", "Eng");
        localStrings.CreateEnglishStrings();
        UpdateLanguage();
    }
    void UpdateLanguage()
    {
        mapDefnPanel.transform.Find("DifficultyLabel").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.DIFFICULTY_LABEL);
        mainMenu.transform.Find("SinglePlayerButton").GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.SINGLE_SELECTION);
        mainMenu.transform.Find("MultiPlayerButton").GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.MULTI_SELECTION);
        mainMenu.transform.Find("OptionsButton").GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.OPTIONS_SELECTION);
        mainMenu.transform.Find("ExitButton").GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.EXIT_SELECTION);
        multiPlayerMenu.transform.Find("JoinGameButton").GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.JOIN_GAME);
        multiPlayerMenu.transform.Find("CreateGameButton").GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.CREATE_GAME);
        optionsMenu.transform.Find("LanguageLabel").GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.LANGUAGE_LABEL);
        hostListPanel.transform.Find("TitlePanel").GetChild(0).GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.HOST_LIST_NAME);
        hostListPanel.transform.Find("TitlePanel").GetChild(1).GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.HOST_LIST_COUNT);
        hostListPanel.transform.Find("TitlePanel").GetChild(2).GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.HOST_LIST_MAP);
        hostListPanel.transform.Find("RefreshButton").GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.REFRESH_BUTTON);
        charDefnPanel.transform.Find("TitleLabel").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.CHAR_DEFINITION_TITLE);
        charDefnPanel.transform.Find("PlayerNameLabel").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.PLAYER_NAME_LABEL);
        mapDefnPanel.transform.Find("TitleLabel").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.MAP_TITLE);
        mapDefnPanel.transform.Find("PlayerCountLabel").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.MAP_DEFINITION_COUNT);
        singlePlayerMenu.transform.Find("SingleGameStart").GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.START_GAME);
        charDefnPanel.transform.Find("CharacterHolder").transform.Find("PlayerTitle").GetComponent<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.PLAYER_ALIEN);
        joinGamePanel.GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.START_GAME);
        createGamePanel.GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.START_GAME);
        backToCreateBtn.GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.BACK_BUTTON);
        backMainBtn.GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.BACK_BUTTON);
        backMultiBtn.GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.BACK_BUTTON);
        backToJoinBtn.GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.BACK_BUTTON);
        hardSelectButton.GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.HARD_SELECTION);
        easySelectButton.GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.EASY_SELECTION);
        mediumSelectButton.GetComponentInChildren<Text>().text = localStrings.GetLocalizedString(LocalizedStrings.LocalKeys.MEDIUM_SELECTION);
    }
    void Update()
    {
        if (mapCountChanged)
        {
           UdpSender us = FindObjectOfType<UdpSender>();
           if (us != null)
           {
               us.currentPlayerCount++;
               mapCountChanged = false;
           }
        }
    }
}
