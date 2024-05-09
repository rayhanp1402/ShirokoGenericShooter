using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseHealth : MonoBehaviour
{
    public int startingHealth = 90;
    protected int currentHealth;

    protected Animator anim;
    protected CapsuleCollider capsuleCollider;
    protected bool isDead;

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        // TODO: Add any common death behavior here

        GetComponent<NavMeshAgent>().enabled = false;

        Statistic statistic = FindObjectOfType<Statistic>();
        if (statistic != null)
        {
            statistic.IncrementKill(); // Memanggil method IncrementKill() dari kelas Statistic
        }
    }

    public int getCurrentHealth()
    {
        return this.currentHealth;
    }
}
