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
    private bool playing = true;
    

    [Client]private void Update()
    {
        if (!hasAuthority)
        {
            return;
        }
        gameObject.GetComponent<Animator>().SetFloat("delay", gameObject.GetComponent<Animator>().GetFloat("delay") - Time.deltaTime);
        if (gameObject.GetComponent<Animator>().GetFloat("delay") <= 0)
        {
            gameObject.GetComponent<Animator>().SetFloat("delay", 20.0f);
        }

        if (!playing)
        {
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.GetComponent<Animator>().SetFloat("delay", 20.0f);
            transform.Translate(avatar.transform.forward * speed * Time.smoothDeltaTime, Space.World);
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.GetComponent<Animator>().SetFloat("delay", 20.0f);
            transform.Translate(- (avatar.transform.forward * speed * Time.smoothDeltaTime), Space.World);
        }

        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<Animator>().SetFloat("delay", 20.0f);
            transform.RotateAround(avatar.transform.position, Vector3.up, -rotate * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<Animator>().SetFloat("delay", 20.0f);
            transform.RotateAround(avatar.transform.position, Vector3.up, rotate * Time.deltaTime);
        }
        

    }

    public void EndGame()
    {
        playing = false;
    }

    public void StartGame()
    {
        playing = true;
    }

    public bool GetPlaying()
    {
        return playing;
    }
}
