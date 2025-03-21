using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Nightmare;
using Unity.VisualScripting;
public class DefaultGun : PausibleObject
{
    public int damage = 30;
    public float range = 100f;
    public float timeBetweenFiring = .08f;
    public float effectsDisplayTime = .2f;
    
    AudioSource fireAudio;
    Light fireLight;
    LineRenderer fireLine;
    GameObject player;
    PlayerMovement playerMovement;

    Ray fireRay;
    RaycastHit fireHit;
    int shootableMask;
    
    float timer;

    public int shotsFired = 0;
    public int shotsHit = 0;


    GameObject weaponHolder;
    GameObject defaultGun;

    Animator anim;


    void Awake()
    {
        fireAudio = GetComponent<AudioSource>();
        fireLight = GetComponent<Light>();
        fireLine = GetComponent<LineRenderer>();

        shootableMask = LayerMask.GetMask("Shootable");

        weaponHolder = transform.parent.gameObject;
        defaultGun = weaponHolder.transform.parent.gameObject;

        playerMovement = transform.root.GetComponent<PlayerMovement>();
        Debug.Log(playerMovement);
        Debug.Log(transform.root.name);

        anim = defaultGun.GetComponent<Animator>();
    }

    void OnDestroy()
    {
        StopPausible();
    }

    public void OneHitKill()
        {
            damage = 9999;
            // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // foreach (GameObject enemy in enemies)
            // {
            //     EnemyBaseHealth enemyHealth = enemy.GetComponent<EnemyBaseHealth>();
            //     enemyHealth.TakeDamage(enemyHealth.getCurrentHealth(), enemy.transform.position);
            // }
        }

    void Update()
    {
        timer += Time.deltaTime;
#if !MOBILE_INPUT
        if (Input.GetButton("Fire1") && timer >= timeBetweenFiring)
        {
            Shoot();
        }
        else
        {
            anim.SetTrigger("Idle");
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

    void Shoot()
    {
        timer = 0f;
        shotsFired++;  

        anim.SetTrigger("Fire");

        fireAudio.Play();
        fireLight.enabled = true;

        fireLine.enabled = true;
        fireLine.SetPosition(0, transform.position);

        fireRay.origin = transform.position;
        fireRay.direction = transform.forward;

        if(Physics.Raycast(fireRay, out fireHit, range, shootableMask, QueryTriggerInteraction.Ignore))
        {
            EnemyHealth enemyHealth = fireHit.collider.GetComponent<EnemyHealth>();
            EnemyPetHealth enemyPetHealth = fireHit.collider.GetComponent <EnemyPetHealth> ();
            
            if (enemyHealth != null )
            {
                enemyHealth.TakeDamage(calculateDamage(), fireHit.point);
                shotsHit++;
            }

            if(enemyPetHealth != null)
            {
                enemyPetHealth.TakeDamage(calculateDamage(), fireHit.point);
            }

            fireLine.SetPosition(1, fireHit.point);
        }
        else
        {
            fireLine.SetPosition(1, fireRay.origin +  fireRay.direction * range);
        }
    }

    private float calculateDamage()
    {
        return damage + playerMovement.baseAttack;
    }
}
