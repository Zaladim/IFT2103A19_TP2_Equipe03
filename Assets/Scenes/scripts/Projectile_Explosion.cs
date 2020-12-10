using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Projectile_Explosion : NetworkBehaviour
{
    
    [SerializeField] private GameObject explosion;
    private void OnCollisionEnter(Collision other)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 1.5f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Player_health hp = hit.GetComponent<Player_health>();

                if (hp != null)
                {
                    hp.TakeDamage(20);
                }
            }
        }

        explode();
    }

    private void Update()
    {
        if (transform.position.y < -1)
        {
            explode();
        }
    }

    private void explode()
    {
        GameObject explod = Instantiate(explosion);
        explod.transform.position = transform.position;
        NetworkServer.Spawn(explod);
        Destroy(gameObject);
    }
}
