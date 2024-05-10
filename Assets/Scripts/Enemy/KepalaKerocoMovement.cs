using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class KepalaKerocoMovement : MonoBehaviour
{
    Transform playerTransform;
    UnityEngine.AI.NavMeshAgent nav;

    public GameObject keroco;

    KepalaKerocoAttack kepalaKerocoAttack;

    Animator anim;

    GameObject player;
    PlayerHealth shirokoHealth;

    public float rotationSpeed = 0.1f;
    public float spawnDistance = 2f;
    public float timeBetweenKerocoSpawn = 25f;
    float timer;
    bool isWalking;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        timer = timeBetweenKerocoSpawn;

        kepalaKerocoAttack = GetComponent<KepalaKerocoAttack>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();

        isWalking = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.transform.position);

        if (timer >= timeBetweenKerocoSpawn)
        {
            SpawnKeroco();
        }

        if (distanceToPlayer <= kepalaKerocoAttack.range || shirokoHealth.currentHealth <= 0)
        {
            isWalking = false;
            nav.isStopped = true;
            anim.SetBool("IsWalking", isWalking);
        }
        else
        {
            isWalking = true;
            nav.isStopped = false;
            nav.SetDestination(playerTransform.position);
            anim.SetBool("IsWalking", isWalking);
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
        rotation *= Quaternion.Euler(0, -90, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
    }
}
