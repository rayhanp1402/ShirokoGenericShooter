using UnityEngine;

namespace Nightmare
{
    public class PetHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public float sinkSpeed = 2.5f;
        public AudioClip deathClip;
        
        public bool godMode = false;

        protected float currentHealth;
        protected Animator anim;
        protected AudioSource enemyAudio;
        // ParticleSystem hitParticles;
        protected CapsuleCollider capsuleCollider;
        // EnemyMovement enemyMovement;

        protected virtual void Awake ()
        {
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
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

        public void TakeDamage (float amount, Vector3 hitPoint)
        {
            if (godMode) 
                return;
            if (!IsDead())
            {
                enemyAudio.Play();
                currentHealth -= amount;

                if (IsDead())
                {
                    Death();
                }
            }
        }

        virtual public void Death ()
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
        }

        public float CurrentHealth()
        {
            return currentHealth;
        }
    }
}