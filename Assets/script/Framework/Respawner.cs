using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{

    public void Despawn(GameObject go, float inSeconds)
    {
      
        go.SetActive(false);
        SecondGameManager.Instance.Timer.Add(() =>
        {
            go.SetActive(true);
        }, inSeconds); 
         
        //GameManager.Instance.timer.Add(ActivateGo, inSeconds);// Delegate methodlar bu şekildede çalışabilir
    }

    void ActivateGo()
    {

    }
}