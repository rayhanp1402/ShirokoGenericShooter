using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nightmare
{
    
    public class MovementOrb : MonoBehaviour
    {
        PlayerMovement playerMovement;

        void Awake()
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerMovement>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player collided with movement orb");
                Pickup(other);
            }
        }

        void Pickup(Collider player)
        {
            playerMovement.SpeedBoost(15f);
            Debug.Log("Picked up movement orb");
            Destroy(gameObject);
        }
    }
}