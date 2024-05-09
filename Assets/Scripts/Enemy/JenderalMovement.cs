using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JenderalMovement : MonoBehaviour
{
    Transform playerTransform;
    NavMeshAgent nav;

    GameObject player;
    ShirokoHealth shirokoHealth;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<ShirokoHealth>();
    }

    void Update()
    {

        if (shirokoHealth.currentHealth <= 0)
        {
            nav.isStopped = true;
        }
        else
        {
            nav.isStopped = false;
            nav.SetDestination(playerTransform.position);
        }
    }
}
