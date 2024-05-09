using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KepalaKerocoAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.8f;
    public float effectsDisplayTime = .2f;
    public int damage = 100;
    public float range = 100f;

    GameObject player;
    ShirokoHealth shirokoHealth;
    Transform shotgun;
    Transform shotgunRender;
    Transform barrelEnd;
    EnemyShotgun shotgunScript;

    public float distanceToPlayer;
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shirokoHealth = player.GetComponent<ShirokoHealth>();
        shotgun = transform.GetChild(1);
        shotgunRender = shotgun.transform.GetChild(0);
        barrelEnd = shotgunRender.transform.GetChild(0);
        shotgunScript = barrelEnd.GetComponent<EnemyShotgun>();
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
            shotgunScript.DisableEffects();
        }
    }

    void Attack()
    {
        timer = 0f;

        if (shirokoHealth.currentHealth > 0)
        {
            shotgunScript.Shoot(range, damage);
        }
    }
}
