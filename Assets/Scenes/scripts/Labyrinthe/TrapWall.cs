using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    private bool up = false;

    private void OnTriggerEnter(Collider other)
    {
        if (up)
        {
            up = false;
        }
        else
        {
            up = true;
        }
    }

    private void Update()
    {
        if (up && wall.transform.position.y <= 1)
        {
            wall.transform.Translate(wall.transform.up * Time.smoothDeltaTime);
        } else if (!up && wall.transform.position.y >= -4)
        {
            wall.transform.Translate(-wall.transform.up * Time.smoothDeltaTime); 
        }

        if (wall.transform.position.y > 1)
        {
            wall.transform.position = new Vector3(wall.transform.position.x, 1, wall.transform.position.z);
        } else if (wall.transform.position.y < -4)
        {
            wall.transform.position = new Vector3(wall.transform.position.x, -4, wall.transform.position.z);
        }
    }
}
