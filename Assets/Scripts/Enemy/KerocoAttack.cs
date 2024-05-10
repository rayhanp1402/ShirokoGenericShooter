using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class KerocoAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.8f;
    public float effectsDisplayTime = .2f;
    public int damage = 50;
    public float range = 2f;

    GameObject player;
    PlayerHealth shirokoHealth;
    Transform sword;
    Transform swordEnd;
    EnemySword swordScript;

    public float distanceToPlayer;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<PlayerHealth>();
        sword = transform.GetChild(2);
        swordEnd = sword.transform.GetChild(0);
        swordScript = swordEnd.GetComponent<EnemySword>();
        timer = timeBetweenAttacks;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= range && timer >= timeBetweenAttacks)
        {
            Attack();
        }

        if (timer >= timeBetweenAttacks * effectsDisplayTime)
        {
            swordScript.DisableEffects();
        }
    }

    void Attack()
    {
        timer = 0f;

        if (shirokoHealth.currentHealth > 0)
        {
            swordScript.Shoot(range);
            shirokoHealth.TakeDamage(damage);
        }
    }
}
