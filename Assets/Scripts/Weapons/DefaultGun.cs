using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Default : MonoBehaviour
{
    public int damage = 30;
    public float range = 100f;
    public float timeBetweenFiring = .08f;
    public float effectsDisplayTime = .2f;
    
    AudioSource fireAudio;
    Light fireLight;
    LineRenderer fireLine;

    Ray fireRay;
    RaycastHit fireHit;
    int shootableMask;
    
    float timer;


    void Awake()
    {
        fireAudio = GetComponent<AudioSource>();
        fireLight = GetComponent<Light>();
        fireLine = GetComponent<LineRenderer>();

        shootableMask = LayerMask.GetMask("Shootable");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenFiring)
        {
            Shoot();
        }

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

        fireAudio.Play();
        fireLight.enabled = true;

        fireLine.enabled = true;
        fireLine.SetPosition(0, transform.position);

        fireRay.origin = transform.position;
        fireRay.direction = transform.forward;

        if(Physics.Raycast(fireRay, out fireHit, range, shootableMask))
        {
            EnemyBaseHealth enemyHealth = fireHit.collider.GetComponent<EnemyBaseHealth>();
            if (enemyHealth != null )
            {
                enemyHealth.TakeDamage(damage, fireHit.point);
            }
            fireLine.SetPosition(1, fireHit.point);
        }
        else
        {
            fireLine.SetPosition(1, fireRay.origin +  fireRay.direction * range);
        }
    }
}
