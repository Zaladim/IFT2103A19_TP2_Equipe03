using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject cannon;

    [SerializeField] private float power = 20.0f;
    [SerializeField] private float cooldown = 5.0f;
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        GameObject target = findTarget();

        if (target)
        {

            GameObject bullet = Instantiate(projectile);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            bullet.transform.position = cannon.transform.position + cannon.transform.up * 2;
            rb.velocity = (getVelocity(target.transform.position, bullet.transform.position, 5.0f));
        }
        cooldown = 5.0f;

    }

    private GameObject findTarget()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Player_Move>().GetPlaying())
            {
                Vector3 diff = player.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = player;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    private Vector3 getVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 direction = distance;
        direction.y = 0.0f;

        float Sy = distance.y;
        float Sxz = direction.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 velocity = direction.normalized;
        velocity *= Vxz;
        velocity.y = Vy;

        return velocity;
    }
    
}
