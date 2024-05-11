using UnityEngine;

namespace Nightmare
{
    public class EnemyPetHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public AudioClip deathClip;

        protected float currentHealth;
        protected Animator anim;
        protected AudioSource enemyAudio;
        protected ParticleSystem hitParticles;
        protected CapsuleCollider capsuleCollider;
        // EnemyMovement enemyMovement;

        virtual protected void Awake ()
        {
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
            // enemyMovement = this.GetComponent<EnemyMovement>();
        }

        void OnEnable()
        {
            currentHealth = startingHealth;
            SetKinematics(false);
        }

        private void SetKinematics(bool isKinematic)
        {
            capsuleCollider.isTrigger = isKinematic;
            capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        }

        void Update ()
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
            return currentHealth <= 0f;
        }

        public void SetPetDead()
        {
            currentHealth = 0;
            Death();
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

        public virtual void Death ()
        {
            EventManager.TriggerEvent("Sound", this.transform.position);
            anim.SetTrigger ("Dead");
            currentHealth = 0;

            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
            StartSinking();
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