using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EscapeController : MonoBehaviour {
    
    [SerializeField] GameObject escapeMenuPanel;
	[SerializeField] Text text ;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    Client c;

    void Start()
    {
        escapeMenuPanel.SetActive(false);
        text.text = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.ESCAPE_ALERT);
        yesButton.GetComponentInChildren<Text>().text = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.YES);
        noButton.GetComponentInChildren<Text>().text = LocalizedStrings.m_LocalizedStrings.GetLocalizedString(LocalizedStrings.LocalKeys.NO);
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoclicked);
    }

    void OnYesClicked()
    {
         if(GameManager.Instance.isSinglePlayer){
             escapeMenuPanel.SetActive(false);
             GameManager.Instance.gameFinished = true;
             SecondGameManager.Instance.Timer.Add(() => { Application.Quit(); }, 3);
         }
            
         else
         {
             c = FindObjectOfType<Client>();
             c.Send("CFIN|");
         }
    }
    void OnNoclicked()
    {
        SecondGameManager.Instance.isPaused = false;
        escapeMenuPanel.SetActive(false);
    }
	// Update is called once per frame
	void Update () {

        if (escapeMenuPanel.activeSelf)
            return;

        if (SecondGameManager.Instance.InputController.escape)
        {
            SecondGameManager.Instance.isPaused = true;
            escapeMenuPanel.SetActive(true);
        }

                    
	}
}
