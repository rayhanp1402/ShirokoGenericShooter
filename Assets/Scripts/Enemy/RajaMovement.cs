using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Nightmare;

public class RajaMovement : MonoBehaviour
{
    Transform playerTransform;
    NavMeshAgent nav;

    GameObject player;
    PlayerHealth shirokoHealth;
    RajaAttack rajaAttack;


    public GameObject keroco;

    public float rotationSpeed = 0.1f;
    public float spawnDistance = 2f;
    public float timeBetweenKerocoSpawn = 15f;

    float timer;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();

        rajaAttack = GetComponent<RajaAttack>();

        timer = timeBetweenKerocoSpawn;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.transform.position);

        if (timer >= timeBetweenKerocoSpawn)
        {
            SpawnKeroco();
        }

        if (distanceToPlayer <= rajaAttack.range || shirokoHealth.currentHealth <= 0)
        {
            // Debug.Log("Raja is stopping");
            nav.isStopped = true;
        }
        else
        {
            // Debug.Log("Raja is moving");
            nav.isStopped = false;
            nav.SetDestination(playerTransform.position);
        }
        FaceTarget(playerTransform.position);
    }

    private void SpawnKeroco()
    {
        timer = 0f;
        Vector3 spawnPosition = transform.position + transform.right * spawnDistance;
        Instantiate(keroco, spawnPosition, Quaternion.identity);
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        rotation *= Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
    }
}
