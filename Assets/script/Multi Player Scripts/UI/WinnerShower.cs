using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerShower : MonoBehaviour {

    [SerializeField] GameObject WinnerShowerPanel;
    [SerializeField] Text text;

    bool gameFinish;
    
	void Start () {
        WinnerShowerPanel.SetActive(false);
	}

    public void SetWinner(string name)
    {
        WinnerShowerPanel.SetActive(true);
        text.text = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.WINNER_SHOW) + " " + name;
        GameManager.Instance.gameFinished = true;
        SecondGameManager.Instance.Timer.Add(() => { Application.Quit(); }, 4f);
    }
}
