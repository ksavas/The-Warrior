using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    [SerializeField] GameObject scoreHolder;
    [SerializeField] EnemyContainer enemyContainer;
    [SerializeField] GameObject hostContainer;
    [SerializeField] Text nameLabel;
    [SerializeField] Text killCountPanel;
    [SerializeField] Text deadCountPanel;

    List<GameClient> allClients;

	void Start () {
        nameLabel.text = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.SCORE_LIST_NAME);
        killCountPanel.text = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.SCORE_LIST_KILLS);
        deadCountPanel.text = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.SCORE_LIST_DEADS);
        if (!GameManager.Instance.isSinglePlayer)
        {
            allClients = GameObject.Find("Client(Clone)").GetComponent<Client>().players;
            AddServerPlayers();
        }
	}
    public void AddPlayer(string name)
    {
        GameObject go = Instantiate(scoreHolder) as GameObject;
        go.transform.SetParent(hostContainer.transform);
        go.transform.Find("NameLabel").GetComponent<Text>().text = name;
        go.transform.Find("KillCountPanel").GetComponent<Text>().text = "0";
        go.transform.Find("DeadCountPanel").GetComponent<Text>().text = "0";

    }
    public void AddEnemy(int index)
    {
            GameObject go = Instantiate(scoreHolder) as GameObject;
            go.transform.SetParent(hostContainer.transform);
            go.transform.Find("NameLabel").GetComponent<Text>().text = enemyContainer.transform.GetChild(index).transform.GetChild(2).transform.name;
            go.transform.Find("KillCountPanel").GetComponent<Text>().text = enemyContainer.transform.GetChild(index).transform.GetComponent<EnemyController>().killScore.ToString();
            go.transform.Find("DeadCountPanel").GetComponent<Text>().text = enemyContainer.transform.GetChild(index).transform.GetComponent<EnemyController>().deadScore.ToString();
    }
    public void AddServerPlayers()
    {
        foreach (GameClient gC in allClients)
        {
            GameObject go = Instantiate(scoreHolder) as GameObject;
            go.transform.SetParent(hostContainer.transform);
            go.transform.Find("NameLabel").GetComponent<Text>().text = gC.name;
            go.transform.Find("KillCountPanel").GetComponent<Text>().text = 0.ToString();
            go.transform.Find("DeadCountPanel").GetComponent<Text>().text = 0.ToString();
        }
        
    }
    public void UpdateDeadScore(int index,int count)
    {
      hostContainer.transform.GetChild(index).transform.Find("DeadCountPanel").GetComponent<Text>().text = count.ToString();        
    }
    public void UpdateKillScore(int index, int count)
    {
      hostContainer.transform.GetChild(index).transform.Find("KillCountPanel").GetComponent<Text>().text = count.ToString();
    }
	void Update () {

        if (GameManager.Instance.gameFinished)
        {
            transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            return;
        }
       
        transform.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
        if (SecondGameManager.Instance.InputController.scoreCounter)
            transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
       
	}
}
