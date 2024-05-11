using UnityEngine;
using UnityEngine.Events;

namespace Nightmare
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public int coinValue = 40;
        public AudioClip deathClip;

        protected float currentHealth;
        protected Animator anim;
        protected AudioSource enemyAudio;
        protected ParticleSystem hitParticles;
        protected CapsuleCollider capsuleCollider;
        protected EnemyMovement enemyMovement;
        public UnityAction OnDeath;
        private ObjectiveManager objectiveManager;

        protected void Start()
        {
            // Find the ObjectiveManagerScript in the scene
            objectiveManager = FindObjectOfType<ObjectiveManager>();
        }

        protected virtual void Awake ()
        {
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
        }

        protected void OnEnable()
        {
            currentHealth = startingHealth;
            SetKinematics(false);
        }

        protected void SetKinematics(bool isKinematic)
        {
            capsuleCollider.isTrigger = isKinematic;
            capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        }

       protected  void Update ()
        {
            if (IsDead())
            {
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
                if (transform.position.y < -10f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        public bool IsDead()
        {
            return (currentHealth <= 0f);
        }

        public void TakeDamage (float amount, Vector3 hitPoint)
        {
            if (!IsDead())
            {
                enemyAudio.Play();
                currentHealth -= amount;

                if (IsDead())
                {
                    Death();
                }
            }
                
            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
        }

        protected virtual void Death ()
        {
            CoinDropManager.DropMoney(transform.position, coinValue);
            EventManager.TriggerEvent("Sound", this.transform.position);
            if (anim)
                anim.SetTrigger ("Dead");

            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
            StartSinking();
            OnDeath?.Invoke();
        }

        public void StartSinking ()
        {
            GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
            SetKinematics(true);

            ScoreManager.score += scoreValue;
            
        }

        public float CurrentHealth()
        {
            return currentHealth;
        }
    }
}