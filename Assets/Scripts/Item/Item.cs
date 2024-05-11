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
        float suckTime = 0.5f;
        float suckTimer = 0.0f;
    
        // Start is called before the first frame update
        void Start()
        {
            player = null;
        }

        void OnEnable()
        {
            player = null;
            isSucked = false;
            suckTimer = 0.0f;
            GetComponent<Collider>().enabled = true;
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
                suckTimer += Time.deltaTime;
                Vector3 point = player.position;
                point.y += 1;
                transform.position = Vector3.Lerp(transform.position, point, suckTimer / suckTime);
                if (Vector3.Distance(transform.position, point) < 0.5f)
                {
                    gameObject.SetActive(false);
                    CoinManager.coins += value;
                    this.gameObject.SetActive(false);
                }
            }
        }

        void OnTriggerEnter(Collider other){
            if (other.gameObject.CompareTag("Player") && player == null)
            {
                player = other.gameObject.transform;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
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
