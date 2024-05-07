using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
public class Item : MonoBehaviour
    {
        public int value;

        Transform player;
        bool isSucked = false;
    
        // Start is called before the first frame update
        void Start()
        {
            player = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isSucked && IsGrounded() && player != null)
            {
                StartSuck();
            }
            if (isSucked)
            {
                transform.position = Vector3.Lerp(transform.position, player.position, 0.1f);
                if (Vector3.Distance(transform.position, player.position) < 0.5f)
                {
                    gameObject.SetActive(false);
                    CoinManager.coins += value;
                }
            }
        }

        void OnTriggerEnter(Collider other){
            if (other.gameObject.tag == "Player" && player == null)
            {
                player = other.gameObject.transform;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                player = null;
            }
        }

        void StartSuck()
        {
            if (player != null && IsGrounded())
            {
                isSucked = true;
                GetComponent<Collider>().enabled = false;
            }
        }


        bool IsGrounded()
        {
            return GetComponent<Rigidbody>().velocity.y == 0;
        }
    }
}
