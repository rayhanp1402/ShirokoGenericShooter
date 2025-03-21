using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class HealerPetBehaviour : PausibleObject
    {

        Transform player;
        PlayerHealth playerHealth;
        NavMeshAgent nav;

        ParticleSystem healEffect;
        bool isHealing = true;

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
                if (playerHealth.currentHealth <= 0)
                {
                    StopHeal();
                    nav.ResetPath();
                    return;
                }
                SetDestination(player.position);
                if (nav.remainingDistance <= nav.stoppingDistance){
                    Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
                    transform.LookAt(lookPos);
                }
                timer += Time.deltaTime;
                healEffect.transform.position = player.position;
                if (timer >= healingInterval)
                {
                    timer -= healingInterval;
                    if (playerHealth.currentHealth < playerHealth.startingHealth && isHealing){
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
        
        public void StopHeal()
        {
            healEffect.Stop();
            isHealing = false;
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