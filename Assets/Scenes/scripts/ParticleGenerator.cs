using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ParticleGenerator : NetworkBehaviour
{
    private float delay = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (delay <= 0)
        {
            Destroy(this.gameObject);
        }

        delay -= Time.deltaTime;
    }
}
