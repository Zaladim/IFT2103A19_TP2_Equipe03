using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player_Cannon : NetworkBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject cannon;

    [SerializeField] private float rotate = 1.0f;
    [Client]void Update()
    {
        if (!hasAuthority)
        {
            return;
        }
        print(cannon.transform.eulerAngles.x);
        if (Input.GetKey(KeyCode.Space) && cannon.transform.eulerAngles.x > 280.0f)
        {
            cannon.transform.Rotate(new Vector3(-rotate, 0.0f, 0.0f) * Time.deltaTime);
            //cannon.transform.RotateAround(cannon.transform.position, Vector3.left, rotate * Time.deltaTime);
        } else if (cannon.transform.eulerAngles.x < 340.0f)
        {
            cannon.transform.Rotate(new Vector3(rotate, 0.0f, 0.0f) * Time.deltaTime);
            //cannon.transform.RotateAround(cannon.transform.position, Vector3.left, -rotate * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject bullet = Instantiate(projectile);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            bullet.transform.position = cannon.transform.position + cannon.transform.forward * 0.5f;
            rb.velocity = cannon.transform.forward * 5.0f;
        }
        
        
    }
}
