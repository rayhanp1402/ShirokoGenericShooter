using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nightmare
{
    public class StrengthOrb : MonoBehaviour
    {
        PlayerShooting playerShooting;
        
        void Awake()
        {
            playerShooting = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerShooting>();
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
            if(playerShooting.maxOrb < 15)
            {
                playerShooting.maxOrb++;
                playerShooting.BoostShooting();
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
