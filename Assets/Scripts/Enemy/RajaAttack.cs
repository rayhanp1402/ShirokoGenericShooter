using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class RajaAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.8f;
    public float effectsDisplayTime = .2f;
    public float range = 100f;
    public float currentRange;

    GameObject player;
    PlayerHealth shirokoHealth;
    ShirokoMovement shirokoMovement;
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

    public float timeBetweenAreaDamage = 2f;

    bool playerInRange;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();
        shirokoMovement = player.GetComponent<ShirokoMovement>();
        shotgun = transform.GetChild(3);
        shotgunRender = shotgun.transform.GetChild(0);
        barrelEnd = shotgunRender.transform.GetChild(0);
        shotgunScript = barrelEnd.GetComponent<EnemyShotgun>();
        enemyStat = GetComponent<EnemyStat>();
        currentRange = range;
        timer = timeBetweenAttacks;

        shootableMask = LayerMask.GetMask("Shootable");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        SightingPlayer();

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= currentRange && timer >= timeBetweenAttacks)
        {
            WeaponAttack();
        }

        if (timer >= timeBetweenAttacks * effectsDisplayTime)
        {
            shotgunScript.DisableEffects();
        }

        if (playerInRange && timer >= timeBetweenAreaDamage)
        {
            AreaAttack();
        }
    }

    void WeaponAttack()
    {
        timer = 0f;

        if (shirokoHealth.currentHealth > 0)
        {
            shotgunScript.Shoot(range, enemyStat.currentAttack);
        }
    }

    void AreaAttack()
    {
        timer = 0f;

        if (shirokoHealth.currentHealth > 0)
        {
            shirokoHealth.TakeDamage(10);
            shirokoMovement.Reduce(2, 2);
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
                currentRange = 0f;
            }

        }
        else
        {
            Debug.Log("Player gone!");
            currentRange = 0f;
        }
    }
}
