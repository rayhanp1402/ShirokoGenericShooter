using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShirokoHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    Animator anim;
    ShirokoMovement shirokoMovement;

    bool isDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        shirokoMovement = GetComponent<ShirokoMovement>();
        currentHealth = startingHealth;
    }

    public void TakeDamage (int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if(currentHealth <= 0 && !isDead )
        {
            Death();
        }
    }

    public void TakeDamageFromShot(int amount, Vector3 hitPoint)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death ()
    {
        isDead = true;

        anim.SetTrigger("Die");

        shirokoMovement.enabled = false;
    }
}
