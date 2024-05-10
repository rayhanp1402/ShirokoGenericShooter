using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class KerocoAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.8f;
    public float effectsDisplayTime = .2f;
    public float range = 2f;

    GameObject player;
    PlayerHealth shirokoHealth;
    Transform sword;
    Transform swordEnd;
    EnemySword swordScript;

    KerocoMovement kerocoMovement;
    PetHealth petHealth;
    EnemyStat enemyStat;

    public float distanceToPlayer;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();
        sword = transform.GetChild(2);
        swordEnd = sword.transform.GetChild(0);
        swordScript = swordEnd.GetComponent<EnemySword>();
        kerocoMovement = GetComponent<KerocoMovement>();
        enemyStat = GetComponent<EnemyStat>();
        timer = timeBetweenAttacks;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= range && timer >= timeBetweenAttacks)
        {
            AttackPlayer();
        }

        if (kerocoMovement.closestPet != null && timer >= timeBetweenAttacks)
        {
            AttackPet();
        }

        if (timer >= timeBetweenAttacks * effectsDisplayTime)
        {
            swordScript.DisableEffects();
        }
    }

    void AttackPlayer()
    {
        timer = 0f;

        if (shirokoHealth.currentHealth > 0)
        {
            swordScript.Shoot(range);
            shirokoHealth.TakeDamage(enemyStat.currentAttack);
        }
    }

    void AttackPet()
    {
        timer = 0f;

        petHealth = kerocoMovement.closestPet.GetComponent<PetHealth>();

        if (petHealth.CurrentHealth() > 0)
        {
            swordScript.Shoot(range);
            petHealth.TakeDamage(damage);
        }
    }

    void AttackPet()
    {
        timer = 0f;

        petHealth = kerocoMovement.closestPet.GetComponent<PetHealth>();

        if (petHealth.CurrentHealth() > 0)
        {
            swordScript.Shoot(range);
            petHealth.TakeDamage(damage);
        }
    }
}
