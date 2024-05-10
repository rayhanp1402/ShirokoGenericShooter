using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class BufferPetBehaviour : PausibleObject
    {

        public Transform owner;
        public float alertDistance = 5f;
        public float defaultStopDistance = 3f;
        public float atkBuffValue = 0.2f;

        Transform player;
        EnemyHealth ownerHealth;
        EnemyStat enemyStat;
        EnemyPetHealth health;
        NavMeshAgent nav;
        Animator anim;
        Transform buffEffect;

        bool IsFleeing = false;
        bool IsOwnerAlive = true;

        public float deathTimer = 0f;

        

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            ownerHealth = owner.GetComponent<EnemyHealth>();
            health = GetComponent<EnemyPetHealth>();
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            enemyStat = owner.GetComponent<EnemyStat>();
            buffEffect = transform.Find("BuffEffect");
            buffEffect.GetComponent<ParticleSystem>().Play();
            StartPausible();
        }

        void Start()
        {
            enemyStat.AddBonusAttackPercent(atkBuffValue);
        }

        void OnEnable()
        {
            nav.enabled = true;
            ClearPath();
            deathTimer = 0f;
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

                if (Vector3.Distance(player.position, transform.position) < alertDistance)
                {
                    IsFleeing = true;
                    nav.stoppingDistance = 0f;
                } else {
                    IsFleeing = false;
                    nav.stoppingDistance = defStopDistance;
                }

                if (IsOwnerAlive){
                    if (!IsFleeing) {
                        // Debug.Log("Following");
                        SetDestination(owner.position);
                        if (nav.remainingDistance <= nav.stoppingDistance){
                            Vector3 lookPos = new Vector3(owner.position.x, transform.position.y, owner.position.z);
                            transform.LookAt(lookPos);
                        }
                    } else {
                        // Debug.Log("Fleeing");
                        Vector3 dirToPlayer = transform.position - player.position;
                        dirToPlayer.Normalize();
                        Vector3 newPos = transform.position + dirToPlayer;
                        transform.LookAt(newPos);
                        SetDestination(newPos);
                    }
                }
                buffEffect.position = owner.position;
                if (ownerHealth.IsDead() && IsOwnerAlive){
                    IsOwnerAlive = false;
                    StopBuff();
                }
                if (ownerHealth.IsDead() && !health.IsDead()){
                    deathTimer += Time.deltaTime;
                    if (deathTimer >= 0.2f)
                    {
                        Dead();
                    }
                }
            }
        }

        public void StopBuff(){
            enemyStat.SubstractBonusAttackPercent(atkBuffValue);
            buffEffect.GetComponent<ParticleSystem>().Stop();
        }

        void Dead(){
            health.Death();
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
                if(nav.velocity.magnitude < 0.15f){
                    anim.SetBool("IsRunning", false);
                } else {
                    anim.SetBool("IsRunning", true);
                }

            }
        }

    }
}