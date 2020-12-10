using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private bool gameStarted = false;
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");


        if (players.Length == 2)
        {
            foreach (var player in players)
            {
                player.GetComponent<Player_Move>().StartGame();
            }

            gameStarted = true;
            enabled = false;
        }

    }

    public bool getStarted()
    {
        return gameStarted;
    }
}
