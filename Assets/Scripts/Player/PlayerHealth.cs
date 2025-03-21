﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class PlayerHealth : MonoBehaviour
    {
        public float startingHealth = 100f;
        public float currentHealth;
        public Slider healthSlider;
        public Image damageImage;
        public AudioClip deathClip;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
        public bool godMode = false;

        Animator anim;
        AudioSource playerAudio;
        PlayerMovement playerMovement;
        GameObject weaponHolder;
        bool isDead;
        bool damaged;
        GameObject gameOverScene;

        void Awake()
        {
            // Setting up the references.
            anim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovement>();
            weaponHolder = GameObject.Find("WeaponHolder");
            gameOverScene = GameObject.Find("GameOverCanvas");
            gameOverScene.SetActive(false);

            ResetPlayer();
        }

        public void ResetPlayer()
        {
            // Set the initial health of the player.
            currentHealth = startingHealth;

            playerMovement.enabled = true;
            weaponHolder.SetActive(true);

            anim.SetBool("IsDead", false);
        }

        public void SetGodMode(bool godMode)
        {
            this.godMode = godMode;
        }

        public float getPlayerHealth()
        {
            return currentHealth;
        }

        void Update()
        {
            // If the player has just been damaged...
            if (damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;
        }

        public void HealOrb()
        {
            currentHealth += startingHealth * 0.2f;
            if (currentHealth > startingHealth){
                currentHealth = startingHealth;
            }
        }


        public void TakeDamage(float amount)
        {
            if (godMode)
                return;

            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            // Set the health bar's value to the current health.
            healthSlider.value = currentHealth;

            // Play the hurt sound effect.
            //playerAudio.Play();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }
        }

        public void TakeDamageFromShot(float amount, Vector3 hitPoint)
        {
            currentHealth -= amount;
            healthSlider.value = currentHealth;

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > startingHealth)
                currentHealth = startingHealth;
            healthSlider.value = currentHealth;
        }

        void Death()
        {
            // Set the death flag so this function won't be called again.
            isDead = true;

            // Turn off any remaining shooting effects.
            // weaponHolder.DisableEffects();

            // Tell the animator that the player is dead.
            anim.SetBool("IsDead", true);

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            //playerAudio.clip = deathClip;
            //playerAudio.Play();

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            weaponHolder.SetActive(false);
            gameOverScene.SetActive(true);
        }

        public void RestartLevel()
        {
            EventManager.TriggerEvent("GameOver");
        }
    }
}