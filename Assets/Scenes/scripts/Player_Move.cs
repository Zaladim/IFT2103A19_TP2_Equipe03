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
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotate = 100.0f;
    [SerializeField] private bool playing = true;
    [Client]private void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        if (!playing)
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

        transform.position =
            new Vector3(avatar.transform.position.x, transform.position.y, avatar.transform.position.z);

    }

    public void EndGame()
    {
        playing = false;
    }

    public bool GetPlaying()
    {
        return playing;
    }
}
