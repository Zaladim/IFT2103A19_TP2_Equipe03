using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using NetworkTransformChild = Mirror.NetworkTransformChild;

public class Player_Cannon : NetworkBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject cannon;

    [SerializeField] private float power = 30.0f;
    [SerializeField] private float cooldown = 0.0f;
    [Client]void Update()
    {
        if (!hasAuthority)
        {
            return;
        }
        
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CmdFire();
            cooldown = 2.0f;
        }
        
        
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(projectile);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.position = cannon.transform.position + cannon.transform.up * 2;
        rb.velocity = cannon.transform.up * power;
        gameObject.GetComponent<Animator>().SetTrigger("Fire");

        NetworkServer.Spawn(bullet);
    }
}
