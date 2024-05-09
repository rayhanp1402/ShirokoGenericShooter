using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KerocoAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.8f;
    public float effectsDisplayTime = .2f;
    public int damage = 50;
    public float range = 2f;

    GameObject player;
    ShirokoHealth shirokoHealth;
    Transform sword;
    Transform swordEnd;
    EnemySword swordScript;

    bool playerInRange;
    public float distanceToPlayer;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<ShirokoHealth>();
        sword = transform.GetChild(2);
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
