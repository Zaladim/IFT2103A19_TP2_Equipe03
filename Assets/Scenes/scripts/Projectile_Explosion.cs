using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Explosion : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        GameObject hit = other.gameObject;
        if (hit.CompareTag("Player"))
        {
            Player_health hp = hit.GetComponent<Player_health>();

            if (hp != null)
            {
                hp.TakeDamage(20);
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
        Destroy(gameObject);
    }
}
