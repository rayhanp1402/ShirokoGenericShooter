using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nightmare
{
    public class HelathOrb : MonoBehaviour
    {
        PlayerHealth playerhealth;

        void Awake(){
            playerhealth = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerHealth>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player collided with health orb");
                Pickup(other);
            }
        }

        void Pickup(Collider player)
        {
            if(playerhealth.currentHealth < playerhealth.startingHealth)
            {
                playerhealth.HealOrb();
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Player already has max health");
            }
        }
    }
}