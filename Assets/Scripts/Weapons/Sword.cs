using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;
public class Sword : MonoBehaviour
{
    public float damage = 50f;
    public float range = 2f;
    public float timeBetweenFiring = .08f;
    public float effectsDisplayTime = .2f;

    PlayerMovement playerMovement;

    AudioSource fireAudio;
    Light fireLight;
    LineRenderer fireLine;

    Ray fireRay;
    RaycastHit fireHit;
    int shootableMask;

    GameObject excalibur;

    Animator anim;

    float timer;


    void Awake()
    {
        fireAudio = GetComponent<AudioSource>();
        fireLight = GetComponent<Light>();
        fireLine = GetComponent<LineRenderer>();

        shootableMask = LayerMask.GetMask("Shootable");

        excalibur = transform.parent.gameObject;
        anim = excalibur.GetComponent<Animator>();

        playerMovement = transform.root.GetComponent<PlayerMovement>();

        timer = timeBetweenFiring;
    }

    void Update()
    {
        timer += Time.deltaTime;
#if !MOBILE_INPUT
        if (Input.GetButton("Fire1") && timer >= timeBetweenFiring)
        {
            Shoot();
        }
#else
        // If there is input on the shoot direction stick and it's time to fire...
        if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
        {
            // ... shoot the gun
            Shoot();
        }
#endif
        if (timer >= timeBetweenFiring * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        fireLine.enabled = false;
        fireLight.enabled = false;
    }

    public void Shoot()
    {
        timer = 0f;

        anim.SetTrigger("Fire");

        fireAudio.Play();
        fireLight.enabled = true;

        fireLine.enabled = true;
        fireLine.SetPosition(0, transform.position);

        fireRay.origin = transform.position;
        fireRay.direction = transform.forward;

        if (Physics.Raycast(fireRay, out fireHit, range, shootableMask, QueryTriggerInteraction.Ignore))
        {
            EnemyHealth enemyHealth = fireHit.collider.GetComponent<EnemyHealth>();
            EnemyPetHealth enemyPetHealth = fireHit.collider.GetComponent <EnemyPetHealth> ();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(calculateDamage(), fireHit.point);
            }
            if(enemyPetHealth != null)
            {
                enemyPetHealth.TakeDamage(calculateDamage(), fireHit.point);
            }
            fireLine.SetPosition(1, fireHit.point);
        }
        else
        {
            fireLine.SetPosition(1, fireRay.origin + fireRay.direction * range);
        }
    }

    private float calculateDamage()
    {
        return damage + playerMovement.baseAttack;
    }
}
