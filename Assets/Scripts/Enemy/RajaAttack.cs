using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class RajaAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.8f;
    public float effectsDisplayTime = .2f;
    public float range = 100f;

    GameObject player;
    PlayerHealth shirokoHealth;
    ShirokoMovement shirokoMovement;
    Transform shotgun;
    Transform shotgunRender;
    Transform barrelEnd;
    EnemyShotgun shotgunScript;
    EnemyStat enemyStat;

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
        timer = timeBetweenAttacks;
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

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= range && timer >= timeBetweenAttacks)
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
}
