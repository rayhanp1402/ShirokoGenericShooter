using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class Shotgun : MonoBehaviour
{
    public int damage = 100;
    public float range = 100f;
    public float timeBetweenFiring = .08f;
    public float effectsDisplayTime = .2f;

    public AudioSource fireAudio;
    public AudioSource pumpAudio;
    public LineRenderer fireLine1;
    public LineRenderer fireLine2;
    public LineRenderer fireLine3;

    Light fireLight;

    Ray fireRay;
    RaycastHit fireHit;
    int shootableMask;

    float timer;


    void Awake()
    {
        fireLight = GetComponent<Light>();

        shootableMask = LayerMask.GetMask("Shootable");

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
        fireLine1.enabled = false;
        fireLine2.enabled = false;
        fireLine3.enabled = false;
        fireLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        fireAudio.Play();
        fireLight.enabled = true;

        pumpAudio.PlayDelayed(0.50f);

        fireLine1.enabled = true;
        fireLine1.SetPosition(0, transform.position);

        fireLine2.enabled = true;
        fireLine2.SetPosition(0, transform.position);

        fireLine3.enabled = true;
        fireLine3.SetPosition(0, transform.position);

        Vector3[] directions = new Vector3[3];

        directions[0] = transform.forward;

        float spreadAngle = 10f;
        Vector3 spreadDirection1 = Quaternion.Euler(0, -spreadAngle, 0) * transform.forward;
        Vector3 spreadDirection2 = Quaternion.Euler(0, spreadAngle, 0) * transform.forward;

        directions[1] = spreadDirection1;
        directions[2] = spreadDirection2;

        for (int i = 0; i < directions.Length; i++)
        {
            fireRay.origin = transform.position;
            fireRay.direction = directions[i];

            if (Physics.Raycast(fireRay, out fireHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = fireHit.collider.GetComponent<EnemyHealth>();
                EnemyPetHealth enemyPetHealth = fireHit.collider.GetComponent <EnemyPetHealth> ();

                if (enemyHealth != null)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, fireHit.point);
                    int adjustedDamage = Mathf.RoundToInt(damage * (1 - distanceToEnemy / range));

                    adjustedDamage = Mathf.Max(0, adjustedDamage);

                    enemyHealth.TakeDamage(adjustedDamage, fireHit.point);
                }

                if(enemyPetHealth != null)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, fireHit.point);
                    int adjustedDamage = Mathf.RoundToInt(damage * (1 - distanceToEnemy / range));

                    adjustedDamage = Mathf.Max(0, adjustedDamage);

                    enemyPetHealth.TakeDamage(adjustedDamage, fireHit.point);
                }

                if (i == 0)
                    fireLine1.SetPosition(1, fireHit.point);
                else if (i == 1)
                    fireLine2.SetPosition(1, fireHit.point);
                else if (i == 2)
                    fireLine3.SetPosition(1, fireHit.point);
            }
            else
            {
                if (i == 0)
                    fireLine1.SetPosition(1, fireRay.origin + fireRay.direction * range);
                else if (i == 1)
                    fireLine2.SetPosition(1, fireRay.origin + fireRay.direction * range);
                else if (i == 2)
                    fireLine3.SetPosition(1, fireRay.origin + fireRay.direction * range);
            }
        }
    }

}
