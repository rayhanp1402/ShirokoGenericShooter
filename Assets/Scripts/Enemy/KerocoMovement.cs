using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class KerocoMovement : MonoBehaviour
{
    Transform playerTransform;
    NavMeshAgent nav;

    GameObject player;
    ShirokoHealth shirokoHealth;

    public float rotationSpeed = 0.1f;

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
        FaceTarget(playerTransform.position);
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        rotation *= Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
    }
}
