using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player_Camera : NetworkBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas hud;

    [Client]void Start()
    {
        if (hasAuthority)
        {
            cam.enabled = true;
            hud.enabled = true;
        }
        
        
        
    }
}
