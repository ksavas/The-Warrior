using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour {

    [SerializeField] ScoreCounter scoreCounter;
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.singlePlayerEnemyCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).transform.GetComponent<EnemyController>().enabled = true;
        }
        for (int i = GameManager.Instance.singlePlayerEnemyCount; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
