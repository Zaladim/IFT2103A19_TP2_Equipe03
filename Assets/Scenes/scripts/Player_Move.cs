using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Mirror;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Player_Move : NetworkBehaviour
{
    [SerializeField] private GameObject avatar;
    [SerializeField] private GameObject cannon;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotate = 5.0f;
    [Client]private void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(avatar.transform.forward * speed * Time.smoothDeltaTime, Space.World);
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(- (avatar.transform.forward * speed * Time.smoothDeltaTime), Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Rotate(-Vector3.up * rotate * Time.deltaTime);
            transform.RotateAround(avatar.transform.position, Vector3.up, -rotate * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(Vector3.up * rotate * Time.deltaTime);
            transform.RotateAround(avatar.transform.position, Vector3.up, rotate * Time.deltaTime);
        }

        cannon.transform.position = avatar.transform.position + new Vector3(0.0f, 0.4f, 0.0f);
        cannon.transform.eulerAngles = new Vector3(cannon.transform.eulerAngles.x, avatar.transform.eulerAngles.y, cannon.transform.eulerAngles.z);

    }
}
