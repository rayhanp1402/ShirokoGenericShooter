using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Nightmare;

public class JenderalMovement : MonoBehaviour
{
    Transform playerTransform;
    NavMeshAgent nav;

    GameObject player;
    PlayerHealth shirokoHealth;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();
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
