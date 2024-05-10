using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Nightmare
{
    public class AttackerPetBehaviour : PausibleObject
    {

        Transform player;
        PlayerHealth playerHealth;
        NavMeshAgent nav;
        Animator anim;
        GameObject enemyTarget;

        EnemyHealth enemyHealth;

        List <GameObject> currentEnemyCollision = new List <GameObject> ();


        public float timer = 0f;
        public int attackDamage = 10;
        public float attackInterval = 1f;

        public float maxDistanceFromPlayer = 10f;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth = player.GetComponent<PlayerHealth>();
            anim = GetComponent<Animator>();
            enemyTarget = null;
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
                    nav.ResetPath();
                    return;
                }

                List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
                
                List<GameObject> enemiesInRange = new List<GameObject>();

                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].GetComponent<EnemyHealth>().CurrentHealth() > 0 && Vector3.Distance(enemies[i].transform.position, player.position) < maxDistanceFromPlayer)
                    {
                        enemiesInRange.Add(enemies[i]);   
                    }
                }
                timer += Time.deltaTime;
                if (enemiesInRange.Count > 0)
                {
                    goToClosestEnemy(enemiesInRange);
                    if (currentEnemyCollision.Contains(enemyTarget) && timer >= attackInterval)
                    {
                        transform.LookAt(enemyTarget.transform);
                        Attack();
                    }
                }
                else
                {
                    nav.stoppingDistance = 2f;
                    SetDestination(player.position);
                }

                Debug.DrawLine(transform.position, enemyTarget.transform.position, Color.red);

                if (enemyHealth.CurrentHealth() <= 0)
                {
                    enemyTarget = null;
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
        
        void OnTriggerEnter (Collider other)
        {
                if (other.CompareTag("Enemy")) // Assuming enemies have the "Enemy" tag
                {
                    currentEnemyCollision.Add(other.gameObject);
                }
        }

        void OnTriggerExit (Collider other)
        {
            if (other.CompareTag("Enemy")) // Assuming enemies have the "Enemy" tag
            {
                currentEnemyCollision.Remove(other.gameObject);
            }
        }

        private void goToClosestEnemy(List<GameObject> enemies)
        {
            if (enemies.Count > 0)
            {
                enemyTarget = enemies[0];
                for (int i = 1; i < enemies.Count; i++)
                {
                    if (Vector3.Distance(enemies[i].transform.position, player.position) < Vector3.Distance(enemyTarget.transform.position, player.position))
                    {
                        enemyTarget = enemies[i];
                    }
                }
                enemyHealth = enemyTarget.GetComponent<EnemyHealth>();
                nav.stoppingDistance = 1f;
                SetDestination(enemyTarget.transform.position);
            }
        }

        private void SetDestination(Vector3 position)
        {
            if (nav.isOnNavMesh)
            {
                nav.SetDestination(position);
                if(nav.velocity.magnitude < 0.15f){
                    anim.SetBool("IsRunning", false);
                } else {
                    anim.SetBool("IsRunning", true);
                }
            }
        }

        private void Attack()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if(enemyHealth.CurrentHealth() > 0)
            {
                // ... damage the player.
                enemyHealth.TakeDamage(attackDamage, enemyTarget.transform.position);
                anim.SetTrigger("Attack");
            }
        }


        private IEnumerator DamageDelay()
        {
            // Wait for the duration of the attack animation
            yield return new WaitForEndOfFrame();

            // Damage the player after the delay
            enemyHealth.TakeDamage(attackDamage, enemyTarget.transform.position);
        }

    }
}