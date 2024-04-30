using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class HealerPet : PausibleObject
    {

        Transform player;
        PlayerHealth playerHealth;
        NavMeshAgent nav;

        ParticleSystem healEffect;

        public float timer = 0f;
        public int healingRate = 10;
        public float healingInterval = 2f;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            healEffect = transform.Find("HealEffect").GetComponent<ParticleSystem>();
            playerHealth = player.GetComponent<PlayerHealth>();

            StartPausible();
        }

        void OnEnable()
        {
            nav.enabled = true;
            ClearPath();
            timer = 0f;
        }

        void ClearPath()
        {
            if (nav.hasPath)
                nav.ResetPath();
        }

        void Update()
        {
            if (!isPaused)
            {
                SetDestination(player.position);
                if (nav.remainingDistance <= nav.stoppingDistance){
                    transform.LookAt(player.position);
                }
                timer += Time.deltaTime;
                healEffect.transform.position = player.position;
                if (timer >= healingInterval)
                {
                    timer -= healingInterval;
                    if (playerHealth.currentHealth < playerHealth.startingHealth){
                        healEffect.Play();
                        playerHealth.Heal(healingRate);
                    }
                }
            }
        }

        void OnDestroy()
        {
            nav.enabled = false;
            StopPausible();
        }

        public override void OnPause()
        {
            if (nav.hasPath)
                nav.isStopped = true;
        }

        public override void OnUnPause()
        {
            if (nav.hasPath)
                nav.isStopped = false;
        }
        
        private void SetDestination(Vector3 position)
        {
            if (nav.isOnNavMesh)
            {
                nav.SetDestination(position);
            }
        }

    }
}