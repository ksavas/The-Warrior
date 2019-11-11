using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour {

    [SerializeField] Text text;
    public Player player;
    PlayerHealth playerHealth;

    public void HandleOnLocalPlayerJoined(Player player)
    {
        playerHealth = player.PlayerHealth;
        playerHealth.OnDamageReceived += PlayerHealth_OnDamageReceived;
        HandleOnHealthChanged();
    }
    void PlayerHealth_OnDamageReceived()
    {
        text.text = playerHealth.hitPointsRemaining.ToString();
    }
    void HandleOnHealthChanged()
    {
        text.text = playerHealth.hitPointsRemaining.ToString();
    }
    public void OnHealthGained()
    {
        HandleOnHealthChanged();
    }
}
