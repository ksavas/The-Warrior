using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] public SpawnPoints[] spawnPoints;
    [SerializeField] GameObject currentPlayer;
    [SerializeField] GameObject ammoCounter;
    [SerializeField] GameObject healthCounter;
    [SerializeField] GameObject jetPackCounter;
    [SerializeField] ScoreCounter scoreCounter;
    [SerializeField] PickUpController pickUpController;

    Client c;

    public int killScore;
    public int deadScore;
    void Awake()
    {
        spawnPoints = transform.Find("PlayerSpawnPointContainer").GetComponentsInChildren<SpawnPoints>();
        if (!GameManager.Instance.isSinglePlayer)
        {
            GetComponent<LocalPlayerControls>().enabled = true;
            return;
        }

       scoreCounter.AddPlayer(GameManager.Instance.singlePlayerName);
    }
    void Start()
    {
        GeneratePlayer();
    }
    public void SetKillScore()
    {
        killScore++;

        if (killScore == 5)
        {
            if (GameManager.Instance.isSinglePlayer)
            {
                GameManager.Instance.gameFinished = true;
                SecondGameManager.Instance.Timer.Add(() => { Application.Quit(); }, 3);
            }
            else
            {
                c = FindObjectOfType<Client>();
                c.Send("CWIN|" +c.clientName);
            }
        }


        if(GameManager.Instance.isSinglePlayer)
            scoreCounter.UpdateKillScore(0, killScore);
        else
            scoreCounter.UpdateKillScore(FindObjectOfType<Client>().clientId, killScore);
    }
    public void SetDeadScore()
    {
        deadScore++;
        if (GameManager.Instance.isSinglePlayer)
            scoreCounter.UpdateDeadScore(0, deadScore);
        else
            scoreCounter.UpdateDeadScore(FindObjectOfType<Client>().clientId, deadScore);
    }
    void GeneratePlayer()
    {
        if(GameManager.Instance.currentcharacterIndex == 0){
            currentPlayer = Instantiate(Resources.Load("PlayerAlien")) as GameObject;
            pickUpController.GenerateAmmo("PlayerAlien");
        }
        else if (GameManager.Instance.currentcharacterIndex == 1){
            currentPlayer = Instantiate(Resources.Load("PlayerSwat")) as GameObject;
            pickUpController.GenerateAmmo("PlayerSwat");
        }
        else if (GameManager.Instance.currentcharacterIndex == 2)
        {
            currentPlayer = Instantiate(Resources.Load("PlayerVillager")) as GameObject;
            pickUpController.GenerateAmmo("PlayerVillager");
        }
        currentPlayer.transform.parent = gameObject.transform;
        ammoCounter.GetComponent<AmmoCounter>().player = currentPlayer.GetComponent<Player>();
        ammoCounter.GetComponent<AmmoCounter>().HandleOnLocalPlayerJoined(currentPlayer.GetComponent<Player>());
        if (GameManager.Instance.isSinglePlayer)
        {
            healthCounter.GetComponent<HealthCounter>().player = currentPlayer.GetComponent<Player>();
            healthCounter.GetComponent<HealthCounter>().HandleOnLocalPlayerJoined(currentPlayer.GetComponent<Player>());
        }
        jetPackCounter.GetComponent<JetPackCounter>().player = currentPlayer.GetComponent<Player>();
        SpawnAtSpawnPoint();
    }
    void SpawnAtSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.GetChild(1).transform.position = spawnPoints[spawnIndex].transform.position;
        transform.GetChild(1).transform.rotation = spawnPoints[spawnIndex].transform.rotation;

    }
}
