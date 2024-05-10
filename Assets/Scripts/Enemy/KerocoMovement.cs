using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Nightmare;

public class KerocoMovement : MonoBehaviour
{
    Transform playerTransform;
    NavMeshAgent nav;

    GameObject player;
    PlayerHealth shirokoHealth;

    List<GameObject> petsInRange = new List<GameObject>();
    public GameObject closestPet;

    public float rotationSpeed = 0.1f;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pet"))
        {
            Debug.Log("Pet enters");
            petsInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pet"))
        {
            Debug.Log("Pet exits");
            petsInRange.Remove(other.gameObject);
        }
    }

    void Update()
    {
        if (shirokoHealth.currentHealth <= 0)
        {
            nav.isStopped = true;
        }
        else
        {
            if (petsInRange.Count > 0)
            {
                closestPet = GetClosestPet();
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
        FaceTarget(playerTransform.position);
    }

    private GameObject GetClosestPet()
    {
        GameObject closestPet = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject pet in petsInRange)
        {
            float distance = Vector3.Distance(transform.position, pet.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPet = pet;
            }
        }
        return closestPet;
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
