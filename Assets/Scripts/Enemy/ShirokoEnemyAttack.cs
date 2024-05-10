using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirokoEnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    ShirokoHealth shirokoHealth;
    ShirokoEnemyHealth shirokoEnemyHealth;

    bool playerInRange;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<ShirokoHealth>();
        shirokoEnemyHealth = GetComponent<ShirokoEnemyHealth>();
        anim = player.GetComponent<Animator>();
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
        
        if(timer >= timeBetweenAttacks && playerInRange && shirokoEnemyHealth.getCurrentHealth() > 0)
        {
            Attack();
        }

        if(shirokoHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    void Attack()
    {
        timer = 0f;

        if(shirokoHealth.currentHealth > 0)
        {
            shirokoHealth.TakeDamage(attackDamage);
        }
    }
}
