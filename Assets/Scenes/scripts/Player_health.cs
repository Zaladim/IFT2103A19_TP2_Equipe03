using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player_health : NetworkBehaviour
{
    [SyncVar (hook = "OnTakeDamage")][SerializeField] public float hp = 100;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject youWin;
    [SerializeField] private ProgressBar hpBar;
    [SerializeField] private ProgressBar hpBar2;

    private void Start()
    {
        hpBar.BarValue = hp;
    }

    [Client]private void Update()
    {
        if (!hasAuthority)
        {
            return;
        }
        
        GameObject otherPlayer = getOtherPlayer();

        if (otherPlayer)
        {
            hpBar2.BarValue = otherPlayer.GetComponent<Player_health>().getHp();
        }
        else
        {
            hpBar2.BarValue = 100;
        }
        
        if (hpBar2.BarValue <= 0)
        {
            youWin.SetActive(true);
            GetComponent<Player_Move>().EndGame();
        }
    }
    
    public void TakeDamage(float damage)
    {
        if (!isServer)
        {
            return;
        }
        
        hp -= damage;
    }

    void OnTakeDamage(float oldHP, float newHP)
    {
        hpBar.BarValue = newHP;

        if (newHP <= 0)
        {
            gameOver.SetActive(true);
            GetComponent<Player_Move>().EndGame();
        }
    }
    
    private GameObject getOtherPlayer()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var player in players)
        {
            if (!player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                return player;
            }
        }

        return null;
    }
    
    public float getHp()
    {
        return hp;
    }
}
