using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.NetworkRoom;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_Collision : NetworkBehaviour
{
    [SerializeField] private ProgressBar hpBar;
    [SerializeField] private ProgressBar hpBar2;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject youWin;

    [SyncVar][SerializeField] private float hp = 100.0f;
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

        hpBar.BarValue = hp;
        
        GameObject otherPlayer = getOtherPlayer();

        if (otherPlayer)
        {
            hpBar2.BarValue = otherPlayer.GetComponent<Player_Collision>().getHp();
        }
        else
        {
            hpBar2.BarValue = 100;
        }
        
        if (hp <= 0)
        {
            gameOver.SetActive(true);
            GetComponent<Player_Move>().EndGame();
        } else if (hpBar2.BarValue <= 0)
        {
            youWin.SetActive(true);
            GetComponent<Player_Move>().EndGame();
        }
    }
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Projectile"))
        {
            hp -= 20;
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
