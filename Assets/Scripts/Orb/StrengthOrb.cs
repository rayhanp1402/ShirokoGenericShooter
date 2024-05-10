using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nightmare
{
    public class StrengthOrb : MonoBehaviour
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
                Debug.Log("Player collided with strength orb");
                Pickup(other);
            }
        }

        void Pickup(Collider player)
        {
            if(playerMovement.orbCount < 15)
            {
                playerMovement.orbCount++;
                playerMovement.BoostShooting();
                Debug.Log("Picked up strength orb");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Player already has max strength orbs");
            }
        }
    }
}
