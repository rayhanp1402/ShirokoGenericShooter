using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class EnemyShotgun : MonoBehaviour
{
    public AudioSource fireAudio;
    public AudioSource pumpAudio;
    public LineRenderer fireLine1;
    public LineRenderer fireLine2;
    public LineRenderer fireLine3;

    Light fireLight;

    Ray fireRay;
    RaycastHit fireHit;
    int shootableMask;


    void Awake()
    {
        fireLight = GetComponent<Light>();

        shootableMask = LayerMask.GetMask("Shootable");
    }

    public void DisableEffects()
    {
        fireLine1.enabled = false;
        fireLine2.enabled = false;
        fireLine3.enabled = false;
        fireLight.enabled = false;
    }

    public void Shoot(float range, int damage)
    {
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
                PlayerHealth shirokoHealth = fireHit.collider.GetComponent<PlayerHealth>();
                if (shirokoHealth != null)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, fireHit.point);
                    int adjustedDamage = Mathf.RoundToInt(damage * (1 - distanceToEnemy / range));

                    adjustedDamage = Mathf.Max(0, adjustedDamage);

                    shirokoHealth.TakeDamageFromShot(adjustedDamage, fireHit.point);
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
