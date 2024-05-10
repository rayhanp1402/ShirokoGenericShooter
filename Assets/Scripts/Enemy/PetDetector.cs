using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDetector : MonoBehaviour
{
    public List<GameObject> petsInRange = new List<GameObject>();
    public GameObject closestPet;


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

    public GameObject GetClosestPet()
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
}
