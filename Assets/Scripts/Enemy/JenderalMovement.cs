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

    Transform petDetector;
    PetDetector petDetectorScript;
    GameObject closestPet;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();

        petDetector = transform.GetChild(0);
        petDetectorScript = petDetector.GetComponent<PetDetector>();
    }

    void Update()
    {

        if (shirokoHealth.currentHealth <= 0)
        {
            nav.isStopped = true;
        }
        else
        {
            if (petDetectorScript.petsInRange.Count > 0)
            {
                closestPet = petDetectorScript.GetClosestPet();
                if (closestPet != null)
                {
                    nav.SetDestination(closestPet.transform.position);
                }
            }
            else
            {
                nav.isStopped = false;
                nav.SetDestination(playerTransform.position);
            }
        }
    }
}
