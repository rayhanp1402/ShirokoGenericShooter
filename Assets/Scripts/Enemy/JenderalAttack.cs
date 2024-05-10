using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class JenderalAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.8f;
    public float effectsDisplayTime = .2f;
    public int damage = 50;
    public float range = 2f;

    public float timeBetweenAreaDamage = 2f;

    GameObject player;
    PlayerHealth shirokoHealth;
    Transform sword;
    Transform swordEnd;
    EnemySword swordScript;

    bool playerInRange;
    public float distanceToPlayer;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();
        sword = transform.GetChild(5);
        swordEnd = sword.transform.GetChild(0);
        swordScript = swordEnd.GetComponent<EnemySword>();
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
            swordScript.DisableEffects();
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
            swordScript.Shoot(range);
            shirokoHealth.TakeDamage(damage);
        }
    }

    void AreaAttack()
    {
        timer = 0f;

        if (shirokoHealth.currentHealth > 0)
        {
            shirokoHealth.TakeDamage(10);
        }
    }
}
