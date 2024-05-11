using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;
public class KepalaKerocoAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.8f;
    public float effectsDisplayTime = .2f;
    public float range = 100f;
    public float currentRange;

    GameObject player;
    PlayerHealth shirokoHealth;
    Transform shotgun;
    Transform shotgunRender;
    Transform barrelEnd;
    EnemyShotgun shotgunScript;
    EnemyStat enemyStat;

    Ray sightRay;
    RaycastHit sightHit;
    int shootableMask;

    public float distanceToPlayer;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();
        shotgun = transform.GetChild(1);
        shotgunRender = shotgun.transform.GetChild(0);
        barrelEnd = shotgunRender.transform.GetChild(0);
        shotgunScript = barrelEnd.GetComponent<EnemyShotgun>();
        enemyStat = GetComponent<EnemyStat>();
        timer = timeBetweenAttacks;

        currentRange = range;

        shootableMask = LayerMask.GetMask("Shootable");
    }

    void Update()
    {
        timer += Time.deltaTime;

        SightingPlayer();

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= currentRange && timer >= timeBetweenAttacks)
        {
            Attack();
        }

        if (timer >= timeBetweenAttacks * effectsDisplayTime)
        {
            shotgunScript.DisableEffects();
        }
    }

    void Attack()
    {
        timer = 0f;

        if (shirokoHealth.currentHealth > 0)
        {
            shotgunScript.Shoot(range, enemyStat.currentAttack);
        }
    }

    private void SightingPlayer()
    {
        sightRay.origin = transform.position;
        sightRay.direction = player.transform.position;
        Debug.Log("SightingPlayer() called");

        if (Physics.Raycast(sightRay, out sightHit, range, shootableMask))
        {
            if (sightHit.collider.name == player.name)
            {
                Debug.Log("Player sighted!");
                currentRange = range;
            }
            else
            {
                Debug.Log("Player gone!");
                currentRange = 0;
            }

        }
        else
        {
            Debug.Log("Player gone!");
            currentRange = 0;
        }
    }
}
